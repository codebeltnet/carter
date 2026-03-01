using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Carter;
using Codebelt.Extensions.Carter.AspNetCore.Text.Json.Assets;
using Codebelt.Extensions.Carter.AspNetCore.Xml;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Cuemon.Extensions.AspNetCore.Xml;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Codebelt.Extensions.Carter.AspNetCore.Text.Json;

/// <summary>
/// Functional tests verifying Carter bootstrapped with <see cref="XmlResponseNegotiator"/> as the sole response negotiator.
/// </summary>
public class XmlResponseNegotiatorTest : Test
{
    public XmlResponseNegotiatorTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task GetStatisticalRegions_ShouldReturnXmlResponse_WhenCarterDefaultsAreDisabled()
    {
        using var response = await MinimalWebHostTestFactory.RunAsync(
            services =>
            {
                services.AddMinimalXmlOptions(o =>
                {
                    o.Settings.Writer.Indent = true;
                });
                services.AddCarter(configurator: c => c
                    .WithModule<WorldModule>()
                    .WithResponseNegotiator<XmlResponseNegotiator>());
                services.AddRouting();
            },
            app =>
            {
                app.UseRouting();
                app.UseEndpoints(endpoints => endpoints.MapCarter());
            },
            _ => { },
            async client =>
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                return await client.GetAsync("/world/statistical-regions");
            });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.StartsWith("application/xml", response.Content.Headers.ContentType?.ToString());

        var body = await response.Content.ReadAsStringAsync();

        Assert.NotEmpty(body);

        TestOutput.WriteLine(body);
    }
}
