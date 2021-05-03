using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorWebAssemblyIdentityServer.WebApp.Models.Identity;
using IX.StandardExtensions.ComponentModel;
using IX.StandardExtensions.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

#nullable enable

namespace BlazorWebAssemblyIdentityServer.WebApp.Controllers.Base
{
    public class RequestChain : DisposableBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ControllerBase _controller;
        private readonly DateTime _timestamp;
        private readonly List<Func<ValueTask<IActionResult?>>> _chainedActions;


        private ApplicationUser? _user;

        internal RequestChain(ControllerBase controller, UserManager<ApplicationUser> userManager)
        {
            Requires.NotNull(out this._controller, controller, nameof(controller));
            Requires.NotNull(out this._userManager, userManager, nameof(userManager));

            this._timestamp = DateTime.UtcNow;
            this._chainedActions = new List<Func<ValueTask<IActionResult?>>>();
        }

        public DateTime Timestamp => this._timestamp;

        public ApplicationUser? User => this._user;

        public RequestChain WithAuthentication()
        {
            this._chainedActions.Add(WithAuthenticationInternal);

            return this;
        }

        public RequestChain WithAuthorization(params string[] roles)
        {
            this._chainedActions.Add(WithAuthorizationInternal);

            return this;

            async ValueTask<IActionResult?> WithAuthorizationInternal()
            {
                if (this._user is null)
                {
                    var result = await this.WithAuthenticationInternal();

                    if (result is not null)
                    {
                        return result;
                    }
                }

                if (this._user is null)
                {
                    return this._controller.Unauthorized();
                }

                var userRoles = await this._userManager.GetRolesAsync(this._user);
                if (roles.Except(userRoles)
                    .Any())
                {
                    return this._controller.Forbid();
                }

                return null;
            }
        }

        public async ValueTask<IActionResult> Execute(Func<IActionResult?> action)
        {
            foreach (var func in this._chainedActions)
            {
                IActionResult? result = await func();

                if (result != null)
                {
                    return result;
                }
            }
            return action() ?? this._controller.Ok();
        }

        public async ValueTask<IActionResult> Execute(Func<ValueTask<IActionResult?>> action)
        {
            foreach (var func in this._chainedActions)
            {
                IActionResult? result = await func();

                if (result != null)
                {
                    return result;
                }
            }
            return await action() ?? this._controller.Ok();
        }

        public async ValueTask<IActionResult> Execute(Func<RequestChain, IActionResult?> action)
        {
            foreach (var func in this._chainedActions)
            {
                IActionResult? result = await func();

                if (result != null)
                {
                    return result;
                }
            }
            return action(this) ?? this._controller.Ok();
        }

        public async ValueTask<IActionResult> Execute(Func<RequestChain, ValueTask<IActionResult?>> action)
        {
            foreach (var func in this._chainedActions)
            {
                IActionResult? result = await func();

                if (result != null)
                {
                    return result;
                }
            }
            return await action(this) ?? this._controller.Ok();
        }

        private async ValueTask<IActionResult?> WithAuthenticationInternal()
        {
            if (this._user is not null)
            {
                return null;
            }

            ClaimsPrincipal cp = this._controller.User;
            if (cp.Identity is not { IsAuthenticated: true, Name: not null or "" })
            {
                return this._controller.Unauthorized();
            }

            var k = await this._userManager.GetUserAsync(cp);

            if (k == null)
            {
                return this._controller.Unauthorized();
            }

            this._user = k;

            return null;
        }
    }
}

#nullable restore