---
uid: Codebelt.Extensions.Carter.AspNetCore.Text.Yaml.YamlResponseNegotiator
summary: *content
---

The `YamlResponseNegotiator` integrates YamlDotNet serialization into the Carter response pipeline. Register it as the response negotiator to have all `HttpResponse.Negotiate()` calls produce YAML output—ideal for tooling, configuration, and DevOps-focused APIs.

[!INCLUDE [availability-modern](../../includes/availability-modern.md)]

### Example

`YamlResponseNegotiator` is constructed with an `IOptions<YamlFormatterOptions>` that controls supported media types and YamlDotNet serialization settings. Before serializing a model, the negotiator tests the client's `Accept` header via `CanHandle`; only when it returns `true` is `Handle` called to produce YAML output. `Handle` resolves the character encoding from the `Accept-Charset` header (falling back to `YamlFormatterOptions.Encoding`), sets the `Content-Type` response header, and streams the serialized YAML directly to the response body.

The following example constructs the negotiator with default options, confirms that the `application/yaml` media type is accepted, then writes an anonymous object to an in-memory response stream and reads back the serialized output:

```csharp
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Codebelt.Extensions.Carter.AspNetCore.Text.Yaml;
using Codebelt.Extensions.YamlDotNet.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace DocfxExample;

public static class YamlResponseNegotiatorExample
{
    public static async Task DemonstrateAsync()
    {
        var options = Options.Create(new YamlFormatterOptions());
        var negotiator = new YamlResponseNegotiator(options);
        var acceptHeader = MediaTypeHeaderValue.Parse("application/yaml");
        if (negotiator.CanHandle(acceptHeader))
        {
            var context = new DefaultHttpContext();
            context.Request.Headers.Accept = "application/yaml";
            var model = new { Name = "YamlDotNet", Value = 42 };
            await negotiator.Handle(context.Request, context.Response, model, CancellationToken.None);
            context.Response.Body.Position = 0;
            using var reader = new StreamReader(context.Response.Body, Encoding.UTF8);
            Console.WriteLine(await reader.ReadToEndAsync());
        }
    }
}
```
