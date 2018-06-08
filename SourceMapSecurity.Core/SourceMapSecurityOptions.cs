using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace SourceMapSecurity.Core.Middleware
{
    public class SourceMapSecurityOptions
    {
        public int DisallowedHttpStatusCode { get; set; } = 403; 

        public Func<HttpContext, Task<bool>> IsAllowedAsync { get; set; }
    }
}
