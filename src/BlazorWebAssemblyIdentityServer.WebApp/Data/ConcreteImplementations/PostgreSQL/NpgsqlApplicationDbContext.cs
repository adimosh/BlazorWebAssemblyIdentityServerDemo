using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlazorWebAssemblyIdentityServer.WebApp.Data.ConcreteImplementations.PostgreSQL
{
    /// <summary>
    /// A concrete implementation of Npgsql in the DB context.
    /// </summary>
    /// <seealso cref="BlazorWebAssemblyIdentityServer.WebApp.Data.ApplicationDbContext" />
    public class NpgsqlApplicationDbContext : ApplicationDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NpgsqlApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="configuration">The configuration.</param>
        public NpgsqlApplicationDbContext(
            DbContextOptions<NpgsqlApplicationDbContext> options,
            IConfiguration configuration)
            : base(options, configuration)
        {
        }

        /// <summary>
        ///     <para>
        ///         Override this method to configure the database (and other options) to be used for this context.
        ///         This method is called for each instance of the context that is created.
        ///         The base implementation does nothing.
        ///     </para>
        ///     <para>
        ///         In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
        ///         to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
        ///         the options have already been set, and skip some or all of the logic in
        ///         <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
        ///     </para>
        /// </summary>
        /// <param name="optionsBuilder">
        ///     A builder used to create or modify options for this context. Databases (and other extensions)
        ///     typically define extension methods on this object that allow you to configure the context.
        /// </param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            base.OnConfiguring(optionsBuilder.UseNpgsql(this.Configuration.GetConnectionString("DefaultConnection")));
    }
}