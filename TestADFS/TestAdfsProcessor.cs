using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ADFS;

namespace TestADFS
{
    public class TestAdfsProcessor : IAdfsAuthProcessor
    {
        public Task TicketCreated(ADFSCreatingTiketContext context)
        {
            ((ClaimsIdentity)context.Principal.Identity).AddClaim(new Claim("ThisIsClaim", "ThisIsClaimValue"));
            return Task.CompletedTask;
        }
    }
}
