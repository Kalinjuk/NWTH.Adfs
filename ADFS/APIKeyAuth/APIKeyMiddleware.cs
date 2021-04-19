using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace ADFS.APIKeyAuth
{
    public class APIKeyMiddleware : IMiddleware
    {
        //private IRouter router;

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            
            //context.GetRouteData()

            throw new NotImplementedException();
        }
    }
}
