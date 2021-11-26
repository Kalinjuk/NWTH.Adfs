using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using ADFS;

namespace nwth.ADFS
{
    public class ADFSHandler<TOptions>: OAuthHandler<TOptions> where TOptions : ADFSOptions, new()
    {
        private readonly IAdfsAuthProcessor _processor;

        public ADFSHandler(IOptionsMonitor<TOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            IAdfsAuthProcessor processor=null)
            : base(options, logger, encoder, clock) {

            _processor = processor;
        }

        

        protected override string BuildChallengeUrl(AuthenticationProperties properties, string redirectUri)
        {

            var scopeParameter = properties.GetParameter<ICollection<string>>(OAuthChallengeProperties.ScopeKey);
            var scope = scopeParameter != null ? FormatScope(scopeParameter) : FormatScope();

            var state = Options.StateDataFormat.Protect(properties);
            var parameters = new Dictionary<string, string>
            {
                { "client_id", Options.ClientId },
                { "scope", scope },
                { "response_type", "code" },
                { "redirect_uri", redirectUri },
                { "state", state },
                { "resource", Options.Resource }
            };

            String S = QueryHelpers.AddQueryString(Options.AuthorizationEndpoint, parameters);

            Logger.LogDebug($"BuildChallengeUrl {S}");

            return QueryHelpers.AddQueryString(Options.AuthorizationEndpoint, parameters);

        }



        protected override async Task<HandleRequestResult> HandleRemoteAuthenticateAsync()
        {
            return await HandleRemoteAuthenticateAsync();
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            
            return base.HandleAuthenticateAsync();
        }

        


        protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            return base.HandleForbiddenAsync(properties);
        }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            //Logger.LogDebug($"Token: {tokens.AccessToken}");

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler
            {
                MapInboundClaims = true
            };

            //Читаем токен
            SecurityToken T = tokenHandler.ReadToken(tokens.AccessToken);
            string KeyName = (string)((JwtSecurityToken)T).Header["x5t"];

            X509Certificate2 Cer = await ADFSMetadata.Instance(Options.Server,Logger).GetCertificate(KeyName);
            X509SecurityKey signingKey = new X509SecurityKey(Cer);
            
            TokenValidationParameters V = new TokenValidationParameters
            {
                ValidAudience = $"microsoft:identityserver:{Options.Resource}",
                IssuerSigningKey = signingKey,
                ValidateIssuerSigningKey = true,
                ValidIssuer =Options.ValidIssuer,
                ValidateIssuer = true,
            };
                        
            

            var principal = tokenHandler.ValidateToken(tokens.AccessToken, V, out T);

            await _processor?.TicketCreated(new ADFSCreatingTiketContext(principal));                                    
           
            return new AuthenticationTicket(principal, properties, Scheme.Name);
        }


    }
}
