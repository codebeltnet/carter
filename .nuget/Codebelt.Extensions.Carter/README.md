# Codebelt.Extensions.Carter

A focused extension layer for Carter with configurable response negotiation primitives and endpoint metadata helpers for ASP.NET Core minimal APIs.

## About

**Codebelt.Extensions.Carter** complements Carter by adding reusable negotiation infrastructure and endpoint convention extensions.

Use this package when you want to:

- Add explicit response metadata from Carter route mappings.
- Build on configurable negotiator abstractions.
- Keep minimal API modules expressive and OpenAPI-friendly.

For concrete JSON, YAML, or XML negotiators, use one of the companion packages listed below.

## CSharp Example

```csharp
using Carter;
using Codebelt.Extensions.Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();

var app = builder.Build();

app.MapGet("/status", () => new StatusResponse("ok"))
    .Produces<StatusResponse>(StatusCodes.Status200OK, "application/json")
    .ProducesProblem(StatusCodes.Status503ServiceUnavailable);

app.MapCarter();
app.Run();

public sealed record StatusResponse(string State);
```

## Related Packages

* [Codebelt.Extensions.Carter](https://www.nuget.org/packages/Codebelt.Extensions.Carter/) 📦
* [Codebelt.Extensions.Carter.AspNetCore.Newtonsoft.Json](https://www.nuget.org/packages/Codebelt.Extensions.Carter.AspNetCore.Newtonsoft.Json/) 📦
* [Codebelt.Extensions.Carter.AspNetCore.Text.Json](https://www.nuget.org/packages/Codebelt.Extensions.Carter.AspNetCore.Text.Json/) 📦
* [Codebelt.Extensions.Carter.AspNetCore.Text.Yaml](https://www.nuget.org/packages/Codebelt.Extensions.Carter.AspNetCore.Text.Yaml/) 📦
* [Codebelt.Extensions.Carter.AspNetCore.Xml](https://www.nuget.org/packages/Codebelt.Extensions.Carter.AspNetCore.Xml/) 📦
