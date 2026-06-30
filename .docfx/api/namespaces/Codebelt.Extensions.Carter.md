---
uid: Codebelt.Extensions.Carter
summary: *content
---
Build Carter-based ASP.NET Core minimal APIs with configurable response negotiation and endpoint metadata. The `Codebelt.Extensions.Carter` namespace gives you the infrastructure to plug content-aware response serialization into the Carter pipeline—letting `HttpResponse.Negotiate()` automatically select the right serializer based on the client's Accept header.

Use this namespace when you want to:<br/>
- Add OpenAPI/OpenAPI-like produces metadata to your endpoints using `Produces<TResponse>` and `Produces` extension methods.<br/>
- Build or integrate a custom response negotiator via `ConfigurableResponseNegotiator<TOptions>`.<br/>
- Choose from format-specific negotiator packages for JSON (Newtonsoft.Json or System.Text.Json), YAML, or XML.

[!INCLUDE [availability-modern](../../includes/availability-modern.md)]

### Start here

For most applications, install one of the format-specific negotiator packages (e.g., `Codebelt.Extensions.Carter.AspNetCore.Newtonsoft.Json`) and register the negotiator in your Carter configuration via `CarterConfigurator.WithResponseNegotiator<T>()`. Then add produces metadata to your endpoints using the `Produces<TResponse>` extension method on `IEndpointConventionBuilder`.

If you need a custom serialization format, derive from `ConfigurableResponseNegotiator<TOptions>` in the `Codebelt.Extensions.Carter.Response` namespace and supply your own formatter.

### Extension Members

|Type|Ext|Methods|
|--:|:-:|---|
|IEndpointConventionBuilder|⬇️|`Produces<TResponse>`, `Produces`|
