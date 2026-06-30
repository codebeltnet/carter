---
uid: Codebelt.Extensions.Carter.Response
summary: *content
---

Build your own response negotiator when the built-in Carter serializers don't cover your media format or serialization requirements. The `Codebelt.Extensions.Carter.Response` namespace gives you `ConfigurableResponseNegotiator<TOptions>`, an abstract generic base class that handles Accept-header matching, character-encoding resolution, and model serialization through `HttpResponse.Negotiate()`—so each derived class only needs to supply its formatter and default encoding.

[!INCLUDE [availability-modern](../../includes/availability-modern.md)]

### When to use

Implement a custom negotiator when your Carter application needs to support a media format not covered by the built-in negotiators, or when you require fine-grained control over serialization settings, supported media types, character encoding, or exception-descriptor formatting. The abstract base handles common concerns so each derived class only supplies the formatter and default encoding.

### Start here

Begin by extending `ConfigurableResponseNegotiator<TOptions>` with a concrete options type that implements `IExceptionDescriptorOptions`, `IContentNegotiation`, and `IValidatableParameterObject`. Override `GetFormatter()` to return the appropriate `StreamFormatter<TOptions>` and `GetDefaultEncoding()` to specify the fallback encoding. Register the negotiator via `CarterConfigurator.WithResponseNegotiator<T>()` when configuring Carter.
