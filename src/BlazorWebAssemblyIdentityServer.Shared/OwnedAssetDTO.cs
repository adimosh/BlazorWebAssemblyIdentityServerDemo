using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorWebAssemblyIdentityServer.Shared
{
    public class OwnedAssetDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public double IndivisibleCommonPart { get; set; }
    }
}
