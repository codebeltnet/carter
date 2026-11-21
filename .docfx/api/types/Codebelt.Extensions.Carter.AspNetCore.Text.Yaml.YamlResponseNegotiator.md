---
uid: Codebelt.Extensions.Carter.AspNetCore.Text.Yaml.YamlResponseNegotiator
example: [*content]
---

The `YamlResponseNegotiator` integrates YamlDotNet serialization into the Carter response pipeline. Register it as the response negotiator to have all `HttpResponse.Negotiate()` calls produce YAML output—ideal for tooling, configuration, and DevOps-focused APIs.

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
    public static async Task Main()
    {
        var options = Options.Create(new YamlFormatterOptions());
        var negotiator = new YamlResponseNegotiator(options);
        var acceptHeader = MediaTypeHeaderValue.Parse("application/yaml");
        if (negotiator.CanHandle(acceptHeader))
        {
            using var body = new MemoryStream();
            var context = new DefaultHttpContext();
            context.Response.Body = body;
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

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="bf3b685f7384b7944901e7d0"><a href="~/run-actions/bf3b685f7384b7944901e7d0/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/bf3b685f7384b7944901e7d0/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
