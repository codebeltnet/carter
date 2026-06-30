---
uid: Codebelt.Extensions.Carter.AspNetCore.Text.Json.JsonResponseNegotiator
summary: *content
---

The `JsonResponseNegotiator` integrates System.Text.Json serialization into the Carter response pipeline. Register it as the response negotiator to have all `HttpResponse.Negotiate()` calls produce JSON output using the modern, high-performance System.Text.Json serializer.

[!INCLUDE [availability-modern](../../includes/availability-modern.md)]

### Example

The following example demonstrates how to register `JsonResponseNegotiator` and negotiate a response model with custom options.

```csharp
using System;
using System.IO;
using System.Text;
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
    public static async Task DemonstrateAsync()
    {
        var options = Options.Create(new JsonFormatterOptions());
        var negotiator = new JsonResponseNegotiator(options);
        var acceptHeader = MediaTypeHeaderValue.Parse("application/json");
        if (negotiator.CanHandle(acceptHeader))
        {
            var context = new DefaultHttpContext();
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
