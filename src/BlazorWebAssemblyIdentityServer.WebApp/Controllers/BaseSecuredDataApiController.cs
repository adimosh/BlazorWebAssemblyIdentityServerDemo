using System;
using System.Threading.Tasks;
using BlazorWebAssemblyIdentityServer.WebApp.Data;
using BlazorWebAssemblyIdentityServer.WebApp.Models.Identity;
using IdentityServerHost.Quickstart.UI;
using IX.StandardExtensions.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebAssemblyIdentityServer.WebApp.Controllers
{
    [SecurityHeaders]
    [Authorize]
    public abstract class BaseSecuredDataApiController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;

        protected BaseSecuredDataApiController(ApplicationDbContext dataContext, UserManager<ApplicationUser> userManager)
        {
            this.DataContext = Requires.NotNull(
                dataContext,
                nameof(dataContext));
            Requires.NotNull(out this.userManager, userManager, nameof(userManager));
        }

        protected ApplicationDbContext DataContext { get; }

        protected async ValueTask<(bool Success, long UserId, string UserName, DateTime Timestamp)> GetCurrentUserTimestamp()
        {
            if (this.User.Identity is not { IsAuthenticated: true, Name: not null or "" })
            {
                return (false, default, default, default);
            }

            string userName = this.User.Identity.Name;

            var k = await this.userManager.GetUserAsync(this.User);

            if (k == null)
            {
                return (false, default, default, default);
            }

            return (true, k.Id, userName, DateTime.UtcNow);
        }
    }
}