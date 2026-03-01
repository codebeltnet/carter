using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Carter;
using Carter.Response;
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Cuemon.Xml.Serialization.Formatters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;
using AspNetCoreMediaType = Microsoft.Net.Http.Headers.MediaTypeHeaderValue;

namespace Codebelt.Extensions.Carter.AspNetCore.Xml;

/// <summary>
/// Tests for the <see cref="XmlResponseNegotiator"/> class.
/// </summary>
public class XmlResponseNegotiatorTest : Test
{
    public XmlResponseNegotiatorTest(ITestOutputHelper output) : base(output)
    {
    }

    [Theory]
    [InlineData("application/xml")]
    [InlineData("text/xml")]
    [InlineData("*/*")]
    public void CanHandle_ShouldReturnTrue_WhenMediaTypeIsSupported(string mediaType)
    {
        var sut = new XmlResponseNegotiator(Options.Create(new XmlFormatterOptions()));

        Assert.True(sut.CanHandle(AspNetCoreMediaType.Parse(mediaType)));
    }

    [Theory]
    [InlineData("application/json")]
    [InlineData("text/yaml")]
    [InlineData("application/yaml")]
    public void CanHandle_ShouldReturnFalse_WhenMediaTypeIsNotSupported(string mediaType)
    {
        var sut = new XmlResponseNegotiator(Options.Create(new XmlFormatterOptions
        {
            SupportedMediaTypes = new List<MediaTypeHeaderValue>
            {
                new("application/xml"),
                new("text/xml")
            }
        }));

        Assert.False(sut.CanHandle(AspNetCoreMediaType.Parse(mediaType)));
    }

    [Fact]
    public async Task Handle_ShouldWriteXmlToResponseBody_WithCorrectContentType()
    {
        using var response = await MinimalWebHostTestFactory.RunAsync(
            services =>
            {
                services.Configure<XmlFormatterOptions>(_ => { });
                services.AddCarter(configurator: c => c.WithResponseNegotiator<XmlResponseNegotiator>());
            },
            app =>
            {
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("/", (HttpResponse res, CancellationToken ct) =>
                        res.Negotiate(new FakeModel { Id = 1, Name = "XML" }, ct));
                });
            },
            _ => { },
            async client =>
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                return await client.GetAsync("/");
            });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.StartsWith("application/xml", response.Content.Headers.ContentType?.ToString());

        var body = await response.Content.ReadAsStringAsync();

        Assert.NotEmpty(body);
        Assert.Contains("<Id>1</Id>", body);
        Assert.Contains("<Name>XML</Name>", body);

        TestOutput.WriteLine(body);
    }
}

internal sealed class FakeModel
{
    public int Id { get; init; }
    public string Name { get; init; }
}
