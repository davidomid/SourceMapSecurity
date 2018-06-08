# SourceMapSecurity

[![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/SourceMapSecurity.Core/1.0.0) 
[![NuGet](https://img.shields.io/nuget/dt/Microsoft.AspNetCore.Mvc.svg)](https://www.nuget.org/packages/SourceMapSecurity.Core/1.0.0)

Easy to use ASP.NET Core middleware for restricting access to JavaScript and CSS source map (.map) files.

This middleware allows you to deploy your source maps to your production environment without worrying about the public from viewing your source maps or debugging JavaScript. 

It works by intercepting HTTP requests for .map files and deciding whether or not they should be displayed to the user, depending on your own rules. 

## Prerequisites
- Your source maps must be external files. This middleware does not help you if you're using inline source maps. 
- The source map file extensions must end in ".map" (i.e. .js.min.map, .css.min.map, etc.). 
- (optional) Generate source maps which contain the contents of the original source files, instead of just listing the file names of the source files and deploying those too.

## Installation
- SourceMapSecurity is available from nuget.org. 

## How to use
All you need to do is add this middleware to your *Configure* method in the *Startup* class.

### Most basic configuration (no options specified). 

```csharp
// Default options, all clients are forbidden from downloading source maps and by 
// default receive a 403 status code. 
app.UseSourceMapSecurity();
```

```csharp
app.UseSourceMapSecurity(new SourceMapSecurityOptions()
{
    // You can modify the HTTP status code returned to the client when they don't have access, 
	// in case you would rather not show that a resource is there at all. 
    DisallowedHttpStatusCode = 404,

    // You can modify this method to determine whether or not source maps should be returned 
	// to the client, based on their HttpContext.
    // Returning true means source maps are allowed. 
    // Returning false means source maps are disallowed. 
    // In this example implementation below, source maps are only allowed if you're logged in, 
	// or in the development environment. 
    IsAllowedAsync = async (context) =>
    {
        if (!env.IsDevelopment() && !context.User.Identity.IsAuthenticated)
        {
            return false;
        }
        return true;
    }
}); 
```


