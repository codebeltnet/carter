# Codebelt.Extensions.Carter.AspNetCore.Text.Json

A System.Text.Json-powered response negotiator for Carter in ASP.NET Core minimal APIs.

## About

**Codebelt.Extensions.Carter.AspNetCore.Text.Json** extends the **Codebelt.Extensions.Carter** package with a dedicated JSON response negotiator for Carter, capable of serializing response models to JSON format using System.Text.Json.

Use this package when you want a lightweight, built-in JSON stack with Carter response negotiation and configurable serializer options.

## CSharp Example

Functional-test style sample (same bootstrapping pattern used by this repository):

```csharp
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Carter;
using Codebelt.Extensions.Carter.AspNetCore.Text.Json;
using Codebelt.Extensions.Carter.Assets;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Cuemon.Extensions.AspNetCore.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using var response = await MinimalWebHostTestFactory.RunAsync(
    services =>
    {
        services.AddMinimalJsonOptions(o =>
        {
            o.Settings.Converters.Insert(0, new JsonStringEnumConverter());
        });
        services.AddCarter(configurator: c => c
            .WithModule<WorldModule>()
            .WithResponseNegotiator<JsonResponseNegotiator>());
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
using Codebelt.Extensions.Carter.AspNetCore.Text.Json;
using Cuemon.Extensions.AspNetCore.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMinimalJsonOptions();
builder.Services.AddCarter(c => c
    .WithResponseNegotiator<JsonResponseNegotiator>());

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
