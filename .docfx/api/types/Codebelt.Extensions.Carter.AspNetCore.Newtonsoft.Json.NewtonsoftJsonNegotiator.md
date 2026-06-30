---
uid: Codebelt.Extensions.Carter.AspNetCore.Newtonsoft.Json.NewtonsoftJsonNegotiator
summary: *content
---

The `NewtonsoftJsonNegotiator` integrates Newtonsoft.Json serialization into the Carter response pipeline. Register it as the response negotiator to have all `HttpResponse.Negotiate()` calls produce JSON output using Newtonsoft.Json's configurable serializer.

[!INCLUDE [availability-modern](../../includes/availability-modern.md)]

### Example

The following example demonstrates how to register `NewtonsoftJsonNegotiator` with a custom converter and negotiate a response model.

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
    public static async Task DemonstrateAsync()
    {
        var options = Options.Create(new NewtonsoftJsonFormatterOptions());
        var negotiator = new NewtonsoftJsonNegotiator(options);
        var acceptHeader = MediaTypeHeaderValue.Parse("application/json");
        if (negotiator.CanHandle(acceptHeader))
        {
            var context = new DefaultHttpContext();
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
