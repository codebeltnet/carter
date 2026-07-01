---
uid: Codebelt.Extensions.Carter.AspNetCore.Xml
summary: *content
---
Add XML response capability to your Carter application with the `Codebelt.Extensions.Carter.AspNetCore.Xml` namespace. The `XmlResponseNegotiator` type, built on `System.Xml.XmlWriter`, produces standards-compliant XML responses through the Carter `HttpResponse.Negotiate()` pipeline.

This negotiator uses `XmlFormatterOptions` to configure XML writer settings, supported media types, and exception-descriptor formatting. It supports the `Accept` and `Accept-Charset` headers for content-type and encoding negotiation, and is suitable for legacy interoperability, SOA-style APIs, and enterprise integration scenarios.

[!INCLUDE [availability-modern](../../includes/availability-modern.md)]

### When to use

Choose `XmlResponseNegotiator` when your API must interoperate with legacy systems that require XML, when the API contract is defined by XSD schemas, or when you need standards-compliant XML output for enterprise integration workflows (e.g., SOAP-adjacent services, BizTalk, or SAP connectors).

### Start here

Install the `Codebelt.Extensions.Carter.AspNetCore.Xml` NuGet package, then register `XmlResponseNegotiator` as the response negotiator in your Carter configuration via `CarterConfigurator.WithResponseNegotiator<T>()`.
