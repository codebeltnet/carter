using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Carter;
using Codebelt.Extensions.AspNetCore.Text.Yaml.Formatters;
using Codebelt.Extensions.Carter.AspNetCore.Text.Json.Assets;
using Codebelt.Extensions.Carter.AspNetCore.Text.Yaml;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Codebelt.Extensions.Carter.AspNetCore.Text.Json;

/// <summary>
/// Functional tests verifying Carter bootstrapped with <see cref="YamlResponseNegotiator"/> as the sole response negotiator.
/// </summary>
public class YamlResponseNegotiatorTest : Test
{
    public YamlResponseNegotiatorTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task GetStatisticalRegions_ShouldReturnYamlResponse_WhenCarterDefaultsAreDisabled()
    {
        using var response = await MinimalWebHostTestFactory.RunAsync(
            services =>
            {
                services.AddYamlFormatterOptions();
                services.AddCarter(configurator: c => c
                    .WithModule<WorldModule>()
                    .WithResponseNegotiator<YamlResponseNegotiator>());
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
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/yaml"));
                return await client.GetAsync("/world/statistical-regions");
            });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.StartsWith("application/yaml", response.Content.Headers.ContentType?.ToString());

        var body = await response.Content.ReadAsStringAsync();

        Assert.NotEmpty(body);

        TestOutput.WriteLine(body);
    }
}
