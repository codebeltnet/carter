# Codebelt.Extensions.Carter.AspNetCore.Newtonsoft.Json

A Newtonsoft.Json-powered response negotiator for Carter in ASP.NET Core minimal APIs.

## About

**Codebelt.Extensions.Carter.AspNetCore.Newtonsoft.Json** extends the **Codebelt.Extensions.Carter** package with a dedicated JSON response negotiator for Carter, capable of serializing response models to JSON format using Newtonsoft.Json.

This package is useful when you need Newtonsoft.Json-specific behavior (for example custom converters, contract resolvers, or legacy JSON compatibility) while keeping Carter modules and content negotiation straightforward.

## CSharp Example

Functional-test style sample (same bootstrapping pattern used by this repository):

```csharp
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Carter;
using Codebelt.Extensions.AspNetCore.Newtonsoft.Json.Formatters;
using Codebelt.Extensions.Carter.AspNetCore.Newtonsoft.Json;
using Codebelt.Extensions.Carter.Assets;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using var response = await MinimalWebHostTestFactory.RunAsync(
    services =>
    {
        services.AddNewtonsoftJsonFormatterOptions(o =>
        {
            o.Settings.Converters.Insert(0, new StringEnumConverter(new DefaultNamingStrategy(), false));
        });
        services.AddCarter(configurator: c => c
            .WithModule<WorldModule>()
            .WithResponseNegotiator<NewtonsoftJsonNegotiator>());
        services.AddRouting();
    },
    app =>
    {
        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapCarter());
    },
    _ => { },
    async client =>
    {
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        return await client.GetAsync("/world/statistical-regions");
    });
```

Program-style usage for production apps (remember to inherit from ICarterModule for your endpoints and add other services as needed):

```csharp
using Carter;
using Codebelt.Extensions.AspNetCore.Newtonsoft.Json.Formatters;
using Codebelt.Extensions.Carter.AspNetCore.Newtonsoft.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNewtonsoftJsonFormatterOptions();
builder.Services.AddCarter(c => c
    .WithResponseNegotiator<NewtonsoftJsonNegotiator>());

var app = builder.Build();
app.MapCarter();
app.Run();
```

## Related Packages

* [Codebelt.Extensions.Carter](https://www.nuget.org/packages/Codebelt.Extensions.Carter/) 📦
* [Codebelt.Extensions.Carter.AspNetCore.Newtonsoft.Json](https://www.nuget.org/packages/Codebelt.Extensions.Carter.AspNetCore.Newtonsoft.Json/) 📦
* [Codebelt.Extensions.Carter.AspNetCore.Text.Json](https://www.nuget.org/packages/Codebelt.Extensions.Carter.AspNetCore.Text.Json/) 📦
* [Codebelt.Extensions.Carter.AspNetCore.Text.Yaml](https://www.nuget.org/packages/Codebelt.Extensions.Carter.AspNetCore.Text.Yaml/) 📦
* [Codebelt.Extensions.Carter.AspNetCore.Xml](https://www.nuget.org/packages/Codebelt.Extensions.Carter.AspNetCore.Xml/) 📦
