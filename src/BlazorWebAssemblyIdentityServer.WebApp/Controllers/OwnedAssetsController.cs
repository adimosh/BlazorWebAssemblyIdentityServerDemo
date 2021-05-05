using System.Linq;
using System.Threading.Tasks;
using BlazorWebAssemblyIdentityServer.Shared;
using BlazorWebAssemblyIdentityServer.WebApp.Data;
using BlazorWebAssemblyIdentityServer.WebApp.Extensions;
using BlazorWebAssemblyIdentityServer.WebApp.Models.Identity;
using BlazorWebAssemblyIdentityServer.WebApp.Models.OwnedAssets;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace BlazorWebAssemblyIdentityServer.WebApp.Controllers
{
    [Route("OwnedAssets")]
    public class OwnedAssetsController : BaseSecuredDataApiController
    {
        public OwnedAssetsController(ApplicationDbContext dataContext, UserManager<ApplicationUser> userManager)
        : base(dataContext,userManager)
        {
        }

        // GET: api/<OwnedAssetsController>
        [HttpGet]
        public async ValueTask<IActionResult> Get(
            [FromQuery] int? skip = null,
            [FromQuery] int? top = null,
            [FromQuery] string orderBy = null,
            [FromQuery] string filter = null) =>
            await this.StartChain()
                .WithAuthentication()
                .Execute(async () => {
                    // Validate by user
                    // TODO

                    // Fetch data
                    var data = (await this.DataContext.OwnedAssets.UseQueryElements(
                            skip,
                            top,
                            orderBy,
                            filter)
                        .ToArrayAsync()).Select(p => (OwnedAssetDTO)p)
                        .ToArray();

                    int count = await this.DataContext.OwnedAssets.UseQueryElements(filter)
                        .CountAsync();

                    // Return data
                    return this.Ok((Count: count, Data: data));
                });

        // GET api/<OwnedAssetsController>/5
        [HttpGet("{id:long}")]
        public async ValueTask<IActionResult> Get(long id) =>
            await this.StartChain()
                .WithAuthentication()
                .Execute(async () =>
                {
                    // Validate by user
                    // TODO

                    // Return data
                    var possibleItem = await this.DataContext.OwnedAssets.FirstOrDefaultAsync(p => p.Id == id);

                    if (possibleItem == null)
                    {
                        return this.NotFound();
                    }

                    return this.Ok((OwnedAssetDTO)possibleItem);
                });

        // POST api/<OwnedAssetsController>
        [HttpPost]
        public async ValueTask<IActionResult> Post([FromBody] OwnedAssetDTO value) =>
            await this.StartChain()
                .WithAuthentication()
                .WithAuthorization(
                    "Administrators",
                    "Leaders")
                .Execute(
                    async chain =>
                    {
                        if (chain.User is not ApplicationUser user)
                        {
                            return this.Unauthorized();
                        }

                        // Validate by user
                        // TODO

                        // Validate data
                        if (value == null)
                        {
                            return this.BadRequest("Data provided is empty or in an invalid format.");
                        }

                        if (value.Name is null or "")
                        {
                            return this.BadRequest("Name of the asset must not be empty.");
                        }

                        if (value.IndivisibleCommonPart < 0)
                        {
                            return this.BadRequest("Indivisible common part must not be less than 0.");
                        }

                        // Create and save data
                        OwnedAsset oa = new()
                        {
                            IndivisibleCommonPart = value.IndivisibleCommonPart,
                            LastChangedAt = chain.Timestamp,
                            LastChangedByUserId = user.Id,
                            Name = value.Name
                        };
                        this.DataContext.OwnedAssets.Add(oa);

                        await this.DataContext.SaveChangesAsync();

                        // Return new DTO
                        return this.Ok(oa);
                    });

        // PUT api/<OwnedAssetsController>/5
        [HttpPut("{id:long}")]
        public async ValueTask<IActionResult> Put(
            long id,
            [FromBody] OwnedAssetDTO value) =>
            await this.StartChain()
                .WithAuthentication()
                .WithAuthorization(
                    "Administrators",
                    "Leaders")
                .Execute(
                    async chain =>
                    {
                        if (chain.User is not ApplicationUser user)
                        {
                            return this.Unauthorized();
                        }

                        // Validate by user
                        // TODO

                        // Validate data
                        if (id <= 0)
                        {
                            return this.BadRequest("ID of asset to update must be a positive integer.");
                        }

                        if (value == null)
                        {
                            return this.BadRequest("Data provided is empty or in an invalid format.");
                        }

                        if (value.Name is null or "")
                        {
                            return this.BadRequest("Name of the asset must not be empty.");
                        }

                        if (value.IndivisibleCommonPart < 0)
                        {
                            return this.BadRequest("Indivisible common part must not be less than 0.");
                        }

                        // Fetch, update and save data
                        var oa = await this.DataContext.OwnedAssets.FirstOrDefaultAsync(p => p.Id == id);

                        if (oa == null)
                        {
                            return this.NotFound();
                        }

                        oa.Name = value.Name;
                        oa.IndivisibleCommonPart = value.IndivisibleCommonPart;
                        oa.LastChangedAt = chain.Timestamp;
                        oa.LastChangedByUserId = user.Id;

                        try
                        {
                            await this.DataContext.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            return this.Conflict();
                        }

                        return this.Ok();
                    });

        // DELETE api/<OwnedAssetsController>/5
        [HttpDelete("{id:long}")]
        public async ValueTask<IActionResult> Delete(long id) =>
            await this.StartChain()
                .WithAuthentication()
                .WithAuthorization(
                    "Administrators",
                    "Leaders")
                .Execute(
                    async () =>
                    {
                        // Validate by user
                        // TODO

                        // Validate data
                        if (id <= 0)
                        {
                            return this.BadRequest("ID of asset to update must be a positive integer.");
                        }

                        int results = await this.DataContext.OwnedAssets.Where(p => p.Id == id)
                            .DeleteAsync();

                        if (results == 0)
                        {
                            return this.Conflict();
                        }

                        return this.Ok();
                    });
    }
}
