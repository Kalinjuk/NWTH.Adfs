using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Authentication.Internal;
using Microsoft.AspNetCore.Authentication;

namespace nwth.ADFS
{
    public class ADFSOptions: OAuthOptions
    {
        private string _server;
        public string Resource { get; set; }

        public string Server
        {
            get { return _server; }
            set
            {
                _server = value;
                AuthorizationEndpoint = $"https://{Server}/adfs/oauth2/authorize";
                TokenEndpoint = $"https://{Server}/adfs/oauth2/token";
            }
        }

        public string ValidIssuer
        {
            get { return $"http://{Server}/adfs/services/trust"; }
        }

        private void init()
        {
            SaveTokens = false;
            this.CorrelationCookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
            CallbackPath = new PathString("/signin");
        }

        public ADFSOptions() { init(); }

        public ADFSOptions(string Server)
        {
            init();
            this.Server = Server;            
        }

        public override void Validate()
        {
            base.Validate();
            if (string.IsNullOrEmpty(Resource))
            {
                throw new ArgumentException("Parametr Resource must be provide");
            }
        }

    }
}
