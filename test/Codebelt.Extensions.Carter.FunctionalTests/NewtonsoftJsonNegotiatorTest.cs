using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Carter;
using Codebelt.Extensions.AspNetCore.Newtonsoft.Json.Formatters;
using Codebelt.Extensions.Carter.AspNetCore.Newtonsoft.Json;
using Codebelt.Extensions.Carter.Assets;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Xunit;

namespace Codebelt.Extensions.Carter;

/// <summary>
/// Functional tests verifying Carter bootstrapped with <see cref="NewtonsoftJsonNegotiator"/> as the sole response negotiator.
/// </summary>
public class NewtonsoftJsonNegotiatorTest : Test
{
    public NewtonsoftJsonNegotiatorTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task GetStatisticalRegions_ShouldReturnJsonResponse_WhenCarterDefaultsAreDisabled()
    {
        using var response = await MinimalWebHostTestFactory.RunAsync(
            services =>
            {
                services.AddNewtonsoftJsonFormatterOptions(o =>
                {
                    o.Settings.Converters.Insert(0, new StringEnumConverter(new DefaultNamingStrategy(), false));
                });
                services.AddCarter(configurator: c => c
                    .WithModule<WorldModule>()
                    .WithResponseNegotiator<NewtonsoftJsonNegotiator>());
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
