using Microsoft.AspNetCore.Identity;

namespace BlazorWebAssemblyIdentityServer.WebApp.Models.Identity
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<long>
    {
    }
}
