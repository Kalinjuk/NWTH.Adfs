using ADFS.APIKeyAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestADFS
{
    public class ApiKeyQuery : IGetApiKeyQuery
    {
        public Task<ApiKey> Execute(string providedApiKey)
        {
            throw new NotImplementedException();
        }
    }
}
