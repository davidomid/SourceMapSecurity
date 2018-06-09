# SourceMapSecurity

Easy to use ASP.NET Core middleware for restricting access to JavaScript and CSS source map (.map) files.

This middleware allows you to deploy your source maps to your production environment without worrying about the public from viewing your source maps or debugging JavaScript. 

It works by intercepting HTTP requests for .map files and deciding whether or not they should be displayed to the user, depending on your own rules. 

## Motivations 
This project exists because using source maps in production is great, as long as the source map files are protected from public access.

Please see [this article](https://www.davidomid.com/using-production-source-maps-securely-in-aspnet-core) for a more detailed explanation on why source maps in production are useful, the problems usually surrounding them and how this middleware solves all of those problems. 

## Prerequisites
- Your source maps must be external files. This middleware does not help you if you're using inline source maps. 
- The source map file extensions must end in ".map" (i.e. .js.min.map, .css.min.map, etc.). 
- (optional) Generate source maps which contain the contents of the original source files, instead of just listing the file paths of the source files and deploying those too. This middleware only protects your source map files, therefore it is highly recommended that you do not deploy your source files separately at all.

## Installation
- SourceMapSecurity.Core is available as a [NuGet package](https://www.nuget.org/packages/SourceMapSecurity.Core) from nuget.org. 

## How to use
All you need to do is add this middleware to your *Configure* method in the *Startup* class. 

**NOTE:** The placement of this middleware in your pipeline is important. You need to make sure this it's added before `app.UseStaticFiles();`,
otherwise it will not restrict access to your source map files. 

### Most basic configuration (no options specified). 

```csharp
// Default options, all clients are forbidden from downloading source maps and by 
// default receive a 403 status code. 
app.UseSourceMapSecurity();
```

### More advanced configuration  

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


