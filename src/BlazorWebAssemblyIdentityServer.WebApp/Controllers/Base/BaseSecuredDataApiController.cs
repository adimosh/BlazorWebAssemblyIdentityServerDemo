using System;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BlazorWebAssemblyIdentityServer.WebApp.Controllers.Base;
using BlazorWebAssemblyIdentityServer.WebApp.Data;
using BlazorWebAssemblyIdentityServer.WebApp.Models.Identity;
using IdentityServerHost.Quickstart.UI;
using IX.StandardExtensions.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebAssemblyIdentityServer.WebApp.Controllers
{
    [SecurityHeaders]
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

        protected RequestChain StartChain()
        {
            return new RequestChain(
                this,
                this.userManager);
        }
    }
}