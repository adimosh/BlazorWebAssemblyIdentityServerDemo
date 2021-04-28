using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityServer.WebApp.Models.OwnedAssets
{
    public class AssetIndivisiblePartCategoryAssociation
    {
        [Key]
        [ForeignKey(nameof(AssetIndivisiblePart))]
        public long AssetIndivisiblePartId { get; set; }

        [Key]
        [ForeignKey(nameof(AssetPartCategory))]
        public long AssetPartCategoryId { get; set; }

        [ForeignKey(nameof(AssetIndivisiblePartId))]
        public AssetIndivisiblePart AssetIndivisiblePart { get; set; }

        [ForeignKey(nameof(AssetPartCategoryId))]
        public AssetPartCategory AssetPartCategory { get; set; }
    }
}
