using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Carter;
using Carter.Response;
using Codebelt.Extensions.AspNetCore.Newtonsoft.Json.Formatters;
using Codebelt.Extensions.Newtonsoft.Json.Formatters;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Xunit;
using AspNetCoreMediaType = Microsoft.Net.Http.Headers.MediaTypeHeaderValue;

namespace Codebelt.Extensions.Carter.AspNetCore.Newtonsoft.Json;

/// <summary>
/// Tests for the <see cref="NewtonsoftJsonNegotiator"/> class.
/// </summary>
public class NewtonsoftJsonNegotiatorTest : Test
{
    public NewtonsoftJsonNegotiatorTest(ITestOutputHelper output) : base(output)
    {
    }

    [Theory]
    [InlineData("application/json")]
    [InlineData("*/*")]
    public void CanHandle_ShouldReturnTrue_WhenMediaTypeIsSupported(string mediaType)
    {
        var sut = new NewtonsoftJsonNegotiator(Options.Create(new NewtonsoftJsonFormatterOptions()));

        Assert.True(sut.CanHandle(AspNetCoreMediaType.Parse(mediaType)));
    }

    [Theory]
    [InlineData("application/xml")]
    [InlineData("text/xml")]
    [InlineData("application/yaml")]
    public void CanHandle_ShouldReturnFalse_WhenMediaTypeIsNotSupported(string mediaType)
    {
        var sut = new NewtonsoftJsonNegotiator(Options.Create(new NewtonsoftJsonFormatterOptions
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
                services.AddNewtonsoftJsonFormatterOptions();
                services.AddCarter(configurator: c => c.WithResponseNegotiator<NewtonsoftJsonNegotiator>());
            },
            app =>
            {
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("/", (HttpResponse res, CancellationToken ct) =>
                        res.Negotiate(new FakeModel { Id = 1, Name = "Newtonsoft" }, ct));
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
        Assert.Contains("Newtonsoft", body);

        TestOutput.WriteLine(body);
    }
}

internal sealed class FakeModel
{
    public int Id { get; init; }
    public string Name { get; init; }
}
