---
uid: Codebelt.Extensions.Carter.AspNetCore.Newtonsoft.Json
summary: *content
---
When your Carter application needs the full power of Newtonsoft.Json—custom contract resolvers, `StringEnumConverter`, `JsonIgnore` attributes, or legacy compatibility mode—reach for the `Codebelt.Extensions.Carter.AspNetCore.Newtonsoft.Json` namespace. The central type, `NewtonsoftJsonNegotiator`, handles JSON response serialization through the Carter `HttpResponse.Negotiate()` pipeline.

This negotiator registers the `application/json` media type and uses a configurable `NewtonsoftJsonFormatterOptions` to control serialization settings, supported media types, and exception-descriptor formatting. It integrates with ASP.NET Core's options pattern and supports the `Accept-Charset` header for character-encoding negotiation.

[!INCLUDE [availability-modern](../../includes/availability-modern.md)]

### When to use

Choose `NewtonsoftJsonNegotiator` when you depend on Newtonsoft.Json features that System.Text.Json does not provide—such as `TypeNameHandling`, custom `JsonConverter` pipelines, `PreserveReferencesHandling`, or `ISerializable` support. This negotiator is also the right choice when migrating an existing Newtonsoft.Json-based codebase to Carter without changing your serialization stack.

### Start here

Install the `Codebelt.Extensions.Carter.AspNetCore.Newtonsoft.Json` NuGet package, then register `NewtonsoftJsonNegotiator` as the response negotiator in your Carter configuration via `CarterConfigurator.WithResponseNegotiator<T>()`.
