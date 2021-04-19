using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace nwth.ADFS
{
    public class ADFSPostConfigureOptions<TOptions, THandler> : IPostConfigureOptions<TOptions>
        where TOptions : ADFSOptions, new()
        where THandler : ADFSHandler<TOptions>

    {
        public void PostConfigure(string name, TOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
