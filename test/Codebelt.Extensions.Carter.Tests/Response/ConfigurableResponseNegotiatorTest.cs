using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit;
using Cuemon.Configuration;
using Cuemon.Diagnostics;
using Cuemon.Net.Http;
using Cuemon.Runtime.Serialization.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Xunit;

namespace Codebelt.Extensions.Carter.Response
{
    public class ConfigurableResponseNegotiatorTest : Test
    {
        public ConfigurableResponseNegotiatorTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void CanHandle_ShouldReturnTrue_WhenAcceptHeaderMatches()
        {
            var options = new FakeOptions();
            options.SupportedMediaTypes = new[] { new System.Net.Http.Headers.MediaTypeHeaderValue("application/json") };
            var negotiator = new FakeResponseNegotiator(options);

            var result = negotiator.CanHandle(new MediaTypeHeaderValue("application/json"));

            Assert.True(result);
            Assert.Equal("application/json", negotiator.ContentType);
        }

        [Fact]
        public void CanHandle_ShouldReturnFalse_WhenAcceptHeaderDoesNotMatch()
        {
            var options = new FakeOptions();
            options.SupportedMediaTypes = new[] { new System.Net.Http.Headers.MediaTypeHeaderValue("application/xml") };
            var negotiator = new FakeResponseNegotiator(options);

            var result = negotiator.CanHandle(new MediaTypeHeaderValue("application/json"));

            Assert.False(result);
        }

        [Fact]
        public void GetEncoding_ShouldReturnPreferredEncoding_FromRequest()
        {
            var options = new FakeOptions();
            var negotiator = new FakeResponseNegotiator(options);

            var context = new DefaultHttpContext();
            context.Request.Headers.Append("Accept-Charset", "utf-16;q=1.0, utf-8;q=0.8");

            var encoding = negotiator.GetEncoding(context.Request);

            Assert.Equal(Encoding.Unicode, encoding);
        }

        [Fact]
        public void GetEncoding_ShouldReturnDefaultEncoding_WhenNoMatch()
        {
            var options = new FakeOptions();
            var negotiator = new FakeResponseNegotiator(options);

            var context = new DefaultHttpContext();
            context.Request.Headers.Append("Accept-Charset", "unknown-encoding;q=1.0");

            var encoding = negotiator.GetEncoding(context.Request);

            Assert.Equal("utf-8", encoding.WebName);
        }

        [Fact]
        public async Task Handle_ShouldWriteToResponse()
        {
            var options = new FakeOptions();
            options.SupportedMediaTypes = new[] { new System.Net.Http.Headers.MediaTypeHeaderValue("text/plain") };
            var negotiator = new FakeResponseNegotiator(options);

            var context = new DefaultHttpContext();
            var ms = new MemoryStream();
            context.Response.Body = ms;

            // Simulate CanHandle to set ContentType
            negotiator.CanHandle(new MediaTypeHeaderValue("text/plain"));

            await negotiator.Handle(context.Request, context.Response, "Hello World", CancellationToken.None);

            var result = Encoding.UTF8.GetString(ms.ToArray());

            Assert.Equal("Hello World", result);
            Assert.Equal("text/plain; charset=utf-8", context.Response.ContentType);
        }
    }

    public class FakeResponseNegotiator : ConfigurableResponseNegotiator<FakeOptions>
    {
        public FakeResponseNegotiator(FakeOptions options) : base(options)
        {
        }

        protected override Encoding GetDefaultEncoding()
        {
            return new UTF8Encoding(false);
        }

        public override StreamFormatter<FakeOptions> GetFormatter()
        {
            return new FakeStreamFormatter(o => { });
        }
    }

    public class FakeOptions : IExceptionDescriptorOptions, IContentNegotiation, IValidatableParameterObject
    {
        public FaultSensitivityDetails SensitivityDetails { get; set; }

        public IReadOnlyCollection<System.Net.Http.Headers.MediaTypeHeaderValue> SupportedMediaTypes { get; set; } = new List<System.Net.Http.Headers.MediaTypeHeaderValue>();

        public void ValidateOptions()
        {
        }
    }

    public class FakeStreamFormatter : StreamFormatter<FakeOptions>
    {
        public FakeStreamFormatter(Action<FakeOptions> setup) : base(setup)
        {
        }

        public override Stream Serialize(object source, Type objectType)
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(source?.ToString());
            writer.Flush();
            ms.Position = 0;
            return ms;
        }

        public override object Deserialize(Stream stream, Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}
