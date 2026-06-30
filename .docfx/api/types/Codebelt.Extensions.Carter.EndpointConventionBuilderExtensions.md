---
uid: Codebelt.Extensions.Carter.EndpointConventionBuilderExtensions
summary: *content
---

Attach typed response metadata to any Carter or minimal API endpoint by calling `Produces<TResponse>` or `Produces` on an `IEndpointConventionBuilder`. Each call records an `IProducesResponseTypeMetadata` entry that OpenAPI tooling reads to document the endpoint's response shape and status codes.

[!INCLUDE [availability-modern](../../includes/availability-modern.md)]

> **Note:** DocFX does not yet generate complete API metadata for C# 14 extension block members ([docfx#11010](https://github.com/dotnet/docfx/issues/11010)). The extension methods themselves are fully functional; the limitation is cosmetic in generated API docs only.

### Example

`Produces<TResponse>` and `Produces` attach `IProducesResponseTypeMetadata` directly to an endpoint builder. `Produces<TResponse>` records the response body type, HTTP status code, and accepted content types; `Produces` (no type parameter) records a status-only response with no body—useful for `404`, `401`, and similar cases. Both are chainable and are typically called on a `RouteHandlerBuilder` inside a Carter module or minimal API route definition.

The following example constructs an endpoint builder directly, calls the extension members through their declaring type, and reads back the attached metadata to show what each call contributes:

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codebelt.Extensions.Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;

namespace DocfxExample;

public static class ProducesExtensionExample
{
    public static void Demonstrate()
    {
        var endpointBuilder = new RouteEndpointBuilder(
            context => Task.CompletedTask,
            RoutePatternFactory.Parse("/api/orders"),
            0);

        var fakeBuilder = new FakeEndpointConventionBuilder();
        EndpointConventionBuilderExtensions.Produces<OrderSummary>(fakeBuilder, StatusCodes.Status200OK, "application/json");
        EndpointConventionBuilderExtensions.Produces(fakeBuilder, StatusCodes.Status404NotFound);

        foreach (var convention in fakeBuilder.Conventions)
        {
            convention(endpointBuilder);
        }

        foreach (var metadata in endpointBuilder.Metadata.OfType<IProducesResponseTypeMetadata>())
        {
            Console.WriteLine(
                $"Status: {metadata.StatusCode}, " +
                $"Type: {metadata.Type?.Name ?? "(none)"}, " +
                $"ContentTypes: {string.Join(", ", metadata.ContentTypes)}");
        }
    }

    private sealed class FakeEndpointConventionBuilder : IEndpointConventionBuilder
    {
        public List<Action<EndpointBuilder>> Conventions { get; } = [];
        public void Add(Action<EndpointBuilder> convention) => Conventions.Add(convention);
    }
}

public sealed record OrderSummary(int Id, string Name);
```

The `Produces<TResponse>` and `Produces` extension members are the same methods used when chaining on a `RouteHandlerBuilder` inside a Carter module: `app.MapGet("/orders/{id}", ...).Produces<OrderSummary>().Produces(404)`.

