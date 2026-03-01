using System.Net;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Carter;
using Codebelt.Extensions.Carter.AspNetCore.Text.Json;
using Codebelt.Extensions.Carter.Assets;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Cuemon.Extensions.AspNetCore.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Codebelt.Extensions.Carter;

/// <summary>
/// Functional tests verifying Carter bootstrapped with <see cref="JsonResponseNegotiator"/> as the sole response negotiator.
/// </summary>
public class JsonResponseNegotiatorTest : Test
{
    public JsonResponseNegotiatorTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task GetStatisticalRegions_ShouldReturnJsonResponse_WhenCarterDefaultsAreDisabled()
    {
        using var response = await MinimalWebHostTestFactory.RunAsync(
            services =>
            {
                services.AddMinimalJsonOptions(o =>
                {
                    o.Settings.Converters.Insert(0, new JsonStringEnumConverter()); // namingPolicy: null = preserves PascalCase
                });
                services.AddCarter(configurator: c => c
                    .WithModule<WorldModule>()
                    .WithResponseNegotiator<JsonResponseNegotiator>());
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
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return await client.GetAsync("/world/statistical-regions");
            });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.StartsWith("application/json", response.Content.Headers.ContentType?.ToString());

        var body = await response.Content.ReadAsStringAsync();

        Assert.NotEmpty(body);

        TestOutput.WriteLine(body);
    }
}
