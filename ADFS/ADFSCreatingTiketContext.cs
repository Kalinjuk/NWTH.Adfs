using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ADFS
{
    public class ADFSCreatingTiketContext
    {
        public ClaimsPrincipal Principal { get; set; }
        
        public ADFSCreatingTiketContext(ClaimsPrincipal principal)
        {
            Principal = principal;
        }
    }
}
