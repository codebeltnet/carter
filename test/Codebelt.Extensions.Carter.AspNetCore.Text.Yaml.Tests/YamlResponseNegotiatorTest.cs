using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Carter;
using Carter.Response;
using Codebelt.Extensions.AspNetCore.Text.Yaml;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Codebelt.Extensions.YamlDotNet.Formatters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Xunit;
using AspNetCoreMediaType = Microsoft.Net.Http.Headers.MediaTypeHeaderValue;

namespace Codebelt.Extensions.Carter.AspNetCore.Text.Yaml;

/// <summary>
/// Tests for the <see cref="YamlResponseNegotiator"/> class.
/// </summary>
public class YamlResponseNegotiatorTest : Test
{
    public YamlResponseNegotiatorTest(ITestOutputHelper output) : base(output)
    {
    }

    [Theory]
    [InlineData("application/yaml")]
    [InlineData("text/yaml")]
    [InlineData("text/plain")]
    [InlineData("*/*")]
    public void CanHandle_ShouldReturnTrue_WhenMediaTypeIsSupported(string mediaType)
    {
        var sut = new YamlResponseNegotiator(Options.Create(new YamlFormatterOptions()));

        Assert.True(sut.CanHandle(AspNetCoreMediaType.Parse(mediaType)));
    }

    [Theory]
    [InlineData("application/json")]
    [InlineData("text/xml")]
    [InlineData("application/xml")]
    public void CanHandle_ShouldReturnFalse_WhenMediaTypeIsNotSupported(string mediaType)
    {
        var sut = new YamlResponseNegotiator(Options.Create(new YamlFormatterOptions
        {
            SupportedMediaTypes = new List<MediaTypeHeaderValue>
            {
                new("application/x-yaml"),
                new("application/yaml"),
                new("text/yaml")
            }
        }));

        Assert.False(sut.CanHandle(AspNetCoreMediaType.Parse(mediaType)));
    }

    [Fact]
    public async Task Handle_ShouldWriteYamlToResponseBody_WithCorrectContentType()
    {
        using var response = await MinimalWebHostTestFactory.RunAsync(
            services =>
            {
                services.AddMinimalYamlOptions();
                services.AddCarter(configurator: c => c.WithResponseNegotiator<YamlResponseNegotiator>());
            },
            app =>
            {
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("/", (HttpResponse res, CancellationToken ct) =>
                        res.Negotiate(new FakeModel { Id = 1, Name = "YAML" }, ct));
                });
            },
            _ => { },
            async client =>
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/yaml"));
                return await client.GetAsync("/");
            });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.StartsWith("application/yaml", response.Content.Headers.ContentType?.ToString());

        var body = await response.Content.ReadAsStringAsync();

        Assert.NotEmpty(body);
        Assert.Contains("id: 1", body);
        Assert.Contains("name: YAML", body);

        TestOutput.WriteLine(body);
    }
}

internal sealed class FakeModel
{
    public int Id { get; init; }
    public string Name { get; init; }
}
