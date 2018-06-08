using Microsoft.AspNetCore.Builder;

namespace SourceMapSecurity.Core.Middleware
{
    public static class SourceMapSecurityMiddlewareExtensions
    {
        public static IApplicationBuilder UseSourceMapSecurity(this IApplicationBuilder builder, SourceMapSecurityOptions options = null)
        {
            return builder.UseMiddleware<SourceMapSecurityMiddleware>(options);
        }
    }
}
