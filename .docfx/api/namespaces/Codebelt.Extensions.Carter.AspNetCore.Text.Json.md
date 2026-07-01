---
uid: Codebelt.Extensions.Carter.AspNetCore.Text.Json
summary: *content
---
Use the `Codebelt.Extensions.Carter.AspNetCore.Text.Json` namespace when your Carter application needs JSON response serialization backed by `System.Text.Json`—the high-performance, allocation-friendly JSON serializer built into the ASP.NET Core runtime. The `JsonResponseNegotiator` type integrates this serializer into the Carter `HttpResponse.Negotiate()` pipeline.

This negotiator uses the `JsonFormatterOptions` to configure serialization behavior, supported media types, and exception-descriptor formatting. It respects the `Accept` and `Accept-Charset` headers for content-type and encoding negotiation.

[!INCLUDE [availability-modern](../../includes/availability-modern.md)]

### When to use

Choose `JsonResponseNegotiator` when you prefer the modern `System.Text.Json` stack for its performance characteristics and native ASP.NET Core integration, when you have no dependency on Newtonsoft.Json-specific features, or when you want to minimize external dependencies beyond the ASP.NET Core framework.

### Start here

Install the `Codebelt.Extensions.Carter.AspNetCore.Text.Json` NuGet package, then register `JsonResponseNegotiator` as the response negotiator in your Carter configuration via `CarterConfigurator.WithResponseNegotiator<T>()`.
