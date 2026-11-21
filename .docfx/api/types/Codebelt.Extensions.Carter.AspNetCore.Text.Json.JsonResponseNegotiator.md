---
uid: Codebelt.Extensions.Carter.AspNetCore.Text.Json.JsonResponseNegotiator
example: [*content]
---

The `JsonResponseNegotiator` integrates System.Text.Json serialization into the Carter response pipeline. Register it as the response negotiator to have all `HttpResponse.Negotiate()` calls produce JSON output using the modern, high-performance System.Text.Json serializer.

To inspect the System.Text.Json output for a response model, pass the requested media type to the negotiator and serialize into a memory stream. The explicit type information resolver allows the formatter to serialize the anonymous model even when the host disables reflection-based serialization by default.

```csharp
using System;
using System.IO;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;
using Codebelt.Extensions.Carter.AspNetCore.Text.Json;
using Cuemon.Extensions.Text.Json.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace DocfxExample;

public static class JsonResponseNegotiatorExample
{
    public static async Task Main()
    {
        var formatterOptions = new JsonFormatterOptions();
        formatterOptions.Settings.TypeInfoResolver = new DefaultJsonTypeInfoResolver();
        var options = Options.Create(formatterOptions);
        var negotiator = new JsonResponseNegotiator(options);
        var acceptHeader = MediaTypeHeaderValue.Parse("application/json");
        if (negotiator.CanHandle(acceptHeader))
        {
            using var body = new MemoryStream();
            var context = new DefaultHttpContext();
            context.Response.Body = body;
            context.Request.Headers.Accept = "application/json";
            var model = new { Name = "System.Text.Json", Value = 42 };
            await negotiator.Handle(context.Request, context.Response, model, CancellationToken.None);
            context.Response.Body.Position = 0;
            using var reader = new StreamReader(context.Response.Body, Encoding.UTF8);
            Console.WriteLine(await reader.ReadToEndAsync());
        }
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="6352b06e7d9ea46088bd7abf"><a href="~/run-actions/6352b06e7d9ea46088bd7abf/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/6352b06e7d9ea46088bd7abf/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
