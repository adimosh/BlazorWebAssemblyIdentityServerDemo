using BlazorWebAssemblyIdentityServer.WebApp.Models.Identity;
using BlazorWebAssemblyIdentityServer.WebApp.Models.OwnedAssets;
using IX.StandardExtensions.Contracts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlazorWebAssemblyIdentityServer.WebApp.Data
{
    /// <summary>
    /// The main DB context of this application.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext{BlazorWebAssemblyIdentityServer.WebApp.Models.Identity.ApplicationUser, BlazorWebAssemblyIdentityServer.WebApp.Models.Identity.ApplicationRole, System.Int64, BlazorWebAssemblyIdentityServer.WebApp.Models.Identity.ApplicationUserClaim, BlazorWebAssemblyIdentityServer.WebApp.Models.Identity.ApplicationUserRole, BlazorWebAssemblyIdentityServer.WebApp.Models.Identity.ApplicationUserLogin, BlazorWebAssemblyIdentityServer.WebApp.Models.Identity.ApplicationRoleClaim, BlazorWebAssemblyIdentityServer.WebApp.Models.Identity.ApplicationUserToken}" />
    public abstract class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long,
        ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        private readonly IConfiguration configuration;

        protected ApplicationDbContext(
            DbContextOptions options,
            IConfiguration configuration)
            : base(options)
        {
            Requires.NotNull(out this.configuration, configuration, nameof(configuration));
        }

        protected IConfiguration Configuration => this.configuration;

        public DbSet<OwnedAsset> OwnedAssets { get; set; }

        public DbSet<AssetIndivisiblePart> AssetIndivisibleParts { get; set; }

        public DbSet<AssetPartCategory> AssetPartCategories { get; set; }

        public DbSet<AssetIndivisiblePartCategoryAssociation> AssetPartCategoryAssociations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AssetIndivisiblePartCategoryAssociation>()
                .HasKey(
                    p => new
                    {
                        p.AssetIndivisiblePartId,
                        p.AssetPartCategoryId
                    });

            builder.Entity<ApplicationRole>()
                .HasData(
                    new ApplicationRole
                    {
                        Id = 1,
                        Name = "Administrators",
                        NormalizedName = "ADMINISTRATORS"
                    },
                    new ApplicationRole
                    {
                        Id = 2,
                        Name = "Supervisors",
                        NormalizedName = "SUPERVISORS"
                    },
                    new ApplicationRole
                    {
                        Id = 3,
                        Name = "Leaders",
                        NormalizedName = "LEADERS"
                    },
                    new ApplicationRole
                    {
                        Id = 4,
                        Name = "Limited",
                        NormalizedName = "LIMITED"
                    },
                    new ApplicationRole
                    {
                        Id = 5,
                        Name = "Banned",
                        NormalizedName = "BANNED"
                    });

            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
