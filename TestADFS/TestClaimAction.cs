using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestADFS
{
    public class TestClaimAction : ClaimAction
    {
        public TestClaimAction() : base("", "") { }

        public override void Run(JsonElement userData, ClaimsIdentity identity, string issuer)
        {
            identity.Claims.Append(new Claim("TESTClaim", "12345454654654"));
        }
    }
}
