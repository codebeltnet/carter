---
uid: Codebelt.Extensions.Carter.AspNetCore.Text.Yaml
summary: *content
---
Enable YAML response negotiation in your Carter application with the `Codebelt.Extensions.Carter.AspNetCore.Text.Yaml` namespace. The `YamlResponseNegotiator` type, powered by YamlDotNet, lets your endpoints produce YAML responses through the standard `HttpResponse.Negotiate()` pipeline.

This negotiator uses `YamlFormatterOptions` to configure serialization settings, supported media types, and character encoding. It is well-suited for tooling, configuration APIs, DevOps scenarios, and any environment where YAML is the preferred interchange format.

[!INCLUDE [availability-modern](../../includes/availability-modern.md)]

### When to use

Choose `YamlResponseNegotiator` when your API consumers expect YAML-formatted responses—common in CI/CD tooling, configuration management, infrastructure-as-code services, and cross-platform build pipelines where YAML's human readability and comment support add value over JSON.

### Start here

Install the `Codebelt.Extensions.Carter.AspNetCore.Text.Yaml` NuGet package, then register `YamlResponseNegotiator` as the response negotiator in your Carter configuration via `CarterConfigurator.WithResponseNegotiator<T>()`.
