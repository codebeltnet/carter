using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Carter;
using Carter.Response;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Cuemon.Extensions.Text.Json.Formatters;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using AspNetCoreMediaType = Microsoft.Net.Http.Headers.MediaTypeHeaderValue;

namespace Codebelt.Extensions.Carter.AspNetCore.Text.Json;

/// <summary>
/// Tests for the <see cref="JsonResponseNegotiator"/> class.
/// </summary>
public class JsonResponseNegotiatorTest : Test
{
    public JsonResponseNegotiatorTest(ITestOutputHelper output) : base(output)
    {
    }

    [Theory]
    [InlineData("application/json")]
    [InlineData("*/*")]
    public void CanHandle_ShouldReturnTrue_WhenMediaTypeIsSupported(string mediaType)
    {
        var sut = new JsonResponseNegotiator(Options.Create(new JsonFormatterOptions()));

        Assert.True(sut.CanHandle(AspNetCoreMediaType.Parse(mediaType)));
    }

    [Theory]
    [InlineData("application/xml")]
    [InlineData("text/xml")]
    [InlineData("application/yaml")]
    public void CanHandle_ShouldReturnFalse_WhenMediaTypeIsNotSupported(string mediaType)
    {
        var sut = new JsonResponseNegotiator(Options.Create(new JsonFormatterOptions
        {
            SupportedMediaTypes = new List<MediaTypeHeaderValue>
            {
                new("application/json")
            }
        }));

        Assert.False(sut.CanHandle(AspNetCoreMediaType.Parse(mediaType)));
    }

    [Fact]
    public async Task Handle_ShouldWriteJsonToResponseBody_WithCorrectContentType()
    {
        using var response = await MinimalWebHostTestFactory.RunAsync(
            services =>
            {
                services.AddSingleton(new JsonFormatterOptions());
                services.AddCarter(configurator: c => c.WithResponseNegotiator<JsonResponseNegotiator>());
            },
            app =>
            {
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("/", (HttpResponse res, CancellationToken ct) =>
                        res.Negotiate(new FakeModel { Id = 1, Name = "Json" }, ct));
                });
            },
            _ => { },
            async client =>
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return await client.GetAsync("/");
            });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.StartsWith("application/json", response.Content.Headers.ContentType?.ToString());

        var body = await response.Content.ReadAsStringAsync();

        Assert.NotEmpty(body);
        Assert.Contains("1", body);
        Assert.Contains("Json", body);

        TestOutput.WriteLine(body);
    }
}

internal sealed class FakeModel
{
    public int Id { get; init; }
    public string Name { get; init; }
}
