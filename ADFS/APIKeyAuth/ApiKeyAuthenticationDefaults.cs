using System;
using System.Collections.Generic;
using System.Text;

namespace ADFS.APIKeyAuth
{
    public static class ApiKeyAuthenticationDefaults
    {
        public const string AuthenticationScheme = "APIKey";
        public const string KeyIdClaymType = "nwth.apikeyAuth.KeyId";
        public const string UserIdClaymType = "nwth.apikeyAuth.UserId";
    }
}
