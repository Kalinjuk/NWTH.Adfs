using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADFS.APIKeyAuth
{
    public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = ApiKeyAuthenticationDefaults.AuthenticationScheme;
        public string Scheme => DefaultScheme;
        public string AuthenticationType = DefaultScheme;
    }
}
