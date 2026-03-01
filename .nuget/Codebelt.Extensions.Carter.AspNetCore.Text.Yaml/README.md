# Codebelt.Extensions.Carter.AspNetCore.Text.Yaml

A YamlDotNet-powered response negotiator for Carter in ASP.NET Core minimal APIs.

## About

**Codebelt.Extensions.Carter.AspNetCore.Text.Yaml** extends the **Codebelt.Extensions.Carter** package with a dedicated YAML response negotiator for Carter, capable of serializing response models to YAML format using YamlDotNet.

Use this package when your API clients require YAML responses and you want to keep Carter modules and content negotiation clean and explicit.

## CSharp Example

Functional-test style sample (same bootstrapping pattern used by this repository):

```csharp
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Carter;
using Codebelt.Extensions.Carter.Assets;
using Codebelt.Extensions.Carter.AspNetCore.Text.Yaml;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using var response = await MinimalWebHostTestFactory.RunAsync(
    services =>
    {
        services.AddMinimalYamlOptions();
        services.AddCarter(configurator: c => c
            .WithModule<WorldModule>()
            .WithResponseNegotiator<YamlResponseNegotiator>());
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
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/yaml"));
        return await client.GetAsync("/world/statistical-regions");
    });
```

Program-style usage for production apps (remember to inherit from ICarterModule for your endpoints and add other services as needed):

```csharp
using Carter;
using Codebelt.Extensions.AspNetCore.Text.Yaml.Formatters;
using Codebelt.Extensions.Carter.AspNetCore.Text.Yaml;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMinimalYamlOptions();
builder.Services.AddCarter(c => c
    .WithResponseNegotiator<YamlResponseNegotiator>());

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
