using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using nwth.ADFS;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ADFSExtensions
    {
        public static AuthenticationBuilder AddADFS(this AuthenticationBuilder builder, string authenticationScheme, Action<ADFSOptions> configureOptions)
            => builder.AddADFS<ADFSOptions, ADFSHandler<ADFSOptions>>(authenticationScheme, configureOptions);
        public static AuthenticationBuilder AddADFS(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<ADFSOptions> configureOptions)
           => builder.AddADFS<ADFSOptions, ADFSHandler<ADFSOptions>>(authenticationScheme, displayName, configureOptions);

        public static AuthenticationBuilder AddADFS<TOptions, THandler>(this AuthenticationBuilder builder, string authenticationScheme, Action<TOptions> configureOptions)
            where TOptions : ADFSOptions, new()
            where THandler : ADFSHandler<TOptions>
            => builder.AddADFS<TOptions, THandler>(authenticationScheme, OAuthDefaults.DisplayName, configureOptions);

        public static AuthenticationBuilder AddADFS<TOptions, THandler>(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<TOptions> configureOptions)
            where TOptions : ADFSOptions, new()
            where THandler : ADFSHandler<TOptions>
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<TOptions>, OAuthPostConfigureOptions<TOptions, THandler>>());
            return builder.AddRemoteScheme<TOptions, THandler>(authenticationScheme, displayName, configureOptions);
        }




        /*
        public static AuthenticationBuilder AddADFS<TOptions, THandler>(this AuthenticationBuilder builder, string authenticationScheme)
            where TOptions : ADFSOptions, new()            
        {
            return builder.AddRemoteScheme<TOptions, THandler>(authenticationScheme, OAuthDefaults.DisplayName, configureOptions);
        }*/


    }
}
