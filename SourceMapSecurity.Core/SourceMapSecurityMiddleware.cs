using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SourceMapSecurity.Core.Middleware
{
    public class SourceMapSecurityMiddleware
    {
        private readonly RequestDelegate next;

        private readonly SourceMapSecurityOptions options;

        public SourceMapSecurityMiddleware(RequestDelegate next, SourceMapSecurityOptions options)
        {
            this.next = next;
            this.options = options;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Func<HttpContext, Task<bool>> isAllowedAsync = options?.IsAllowedAsync;

            bool isAllowed = true;

            if(isAllowedAsync != null)
            {
                string fileExtension = Path.GetExtension(context.Request.Path);
                if (fileExtension == ".map")
                {
                    isAllowed = await options.IsAllowedAsync(context);
                }
            }
            else
            {
                isAllowed = false;
            }

            if (isAllowed)
            {
                await this.next.Invoke(context);
            }
        }
    }
}
