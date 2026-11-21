---
uid: Codebelt.Extensions.Carter.AspNetCore.Xml.XmlResponseNegotiator
example: [*content]
---

The `XmlResponseNegotiator` integrates System.Xml XML serialization into the Carter response pipeline. Register it as the response negotiator to have all `HttpResponse.Negotiate()` calls produce standards-compliant XML output—suitable for legacy interoperability and enterprise integration scenarios.

### Example

`XmlResponseNegotiator` is constructed with an `IOptions<XmlFormatterOptions>` that controls supported media types and System.Xml serialization settings, including writer encoding. Before serializing a model, the negotiator tests the client's `Accept` header via `CanHandle`; only when it returns `true` is `Handle` called to produce XML output. `Handle` resolves the character encoding from the `Accept-Charset` header (falling back to `XmlFormatterOptions.Settings.Writer.Encoding`), sets the `Content-Type` response header, and streams the serialized XML directly to the response body.

The following example constructs the negotiator with default options, confirms that the `application/xml` media type is accepted, then writes a public model to an in-memory response stream and reads back the serialized output:

```csharp
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Codebelt.Extensions.Carter.AspNetCore.Xml;
using Cuemon.Xml.Serialization.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace DocfxExample;

public static class XmlResponseNegotiatorExample
{
    public static async Task Main()
    {
        var options = Options.Create(new XmlFormatterOptions());
        var negotiator = new XmlResponseNegotiator(options);
        var acceptHeader = MediaTypeHeaderValue.Parse("application/xml");
        if (negotiator.CanHandle(acceptHeader))
        {
            using var body = new MemoryStream();
            var context = new DefaultHttpContext();
            context.Response.Body = body;
            context.Request.Headers.Accept = "application/xml";
            var model = new Report { Name = "XML", Value = 42 };
            await negotiator.Handle(context.Request, context.Response, model, CancellationToken.None);
            context.Response.Body.Position = 0;
            using var reader = new StreamReader(context.Response.Body, Encoding.UTF8);
            Console.WriteLine(await reader.ReadToEndAsync());
        }
    }
}

public sealed class Report
{
    public string Name { get; set; } = string.Empty;
    public int Value { get; set; }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="f53010d9cc9ec55bc07adac2"><a href="~/run-actions/f53010d9cc9ec55bc07adac2/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/f53010d9cc9ec55bc07adac2/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
