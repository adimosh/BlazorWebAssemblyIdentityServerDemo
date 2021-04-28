using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using BlazorWebAssemblyIdentityServer.WebApp.Models.Identity;

namespace BlazorWebAssemblyIdentityServer.WebApp.Models.OwnedAssets
{
    public class OwnedAsset
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        public double IndivisibleCommonPart { get; set; }

        [ConcurrencyCheck]
        public DateTime LastChangedAt { get; set; }

        [ForeignKey(nameof(LastChangedByUser))]
        public long LastChangedByUserId { get; set; }

        [ForeignKey(nameof(LastChangedByUserId))]
        public ApplicationUser LastChangedByUser { get; set; }
    }
}
