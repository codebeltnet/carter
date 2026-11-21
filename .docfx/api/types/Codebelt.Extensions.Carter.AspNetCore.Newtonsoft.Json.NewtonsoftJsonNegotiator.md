---
uid: Codebelt.Extensions.Carter.AspNetCore.Newtonsoft.Json.NewtonsoftJsonNegotiator
example: [*content]
---

The `NewtonsoftJsonNegotiator` integrates Newtonsoft.Json serialization into the Carter response pipeline. Register it as the response negotiator to have all `HttpResponse.Negotiate()` calls produce JSON output using Newtonsoft.Json's configurable serializer.

### Example

To inspect the JSON a Carter client would receive, pass the requested media type to the negotiator and serialize a response into a memory stream. The stream makes the response body readable without starting a server.

```csharp
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Codebelt.Extensions.Carter.AspNetCore.Newtonsoft.Json;
using Codebelt.Extensions.Newtonsoft.Json.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace DocfxExample;

public static class NewtonsoftJsonNegotiatorExample
{
    public static async Task Main()
    {
        var options = Options.Create(new NewtonsoftJsonFormatterOptions());
        var negotiator = new NewtonsoftJsonNegotiator(options);
        var acceptHeader = MediaTypeHeaderValue.Parse("application/json");
        if (negotiator.CanHandle(acceptHeader))
        {
            using var body = new MemoryStream();
            var context = new DefaultHttpContext();
            context.Response.Body = body;
            context.Request.Headers.Accept = "application/json";
            var model = new { Name = "Newtonsoft", Value = 42 };
            await negotiator.Handle(context.Request, context.Response, model, CancellationToken.None);
            context.Response.Body.Position = 0;
            using var reader = new StreamReader(context.Response.Body, Encoding.UTF8);
            Console.WriteLine(await reader.ReadToEndAsync());
        }
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="93c4f046477745c9b8272f18"><a href="~/run-actions/93c4f046477745c9b8272f18/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/93c4f046477745c9b8272f18/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
