using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ADFS.APIKeyAuth
{
    public interface IGetApiKeyQuery
    {
        Task<ApiKey> Execute(string providedApiKey);
    }
}
