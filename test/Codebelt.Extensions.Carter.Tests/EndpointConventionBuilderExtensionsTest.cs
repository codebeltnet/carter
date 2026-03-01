using System;
using System.Collections.Generic;
using System.Linq;
using Codebelt.Extensions.Xunit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Xunit;

namespace Codebelt.Extensions.Carter
{
    /// <summary>
    /// Tests for the <see cref="EndpointConventionBuilderExtensions"/> class.
    /// </summary>
    public class EndpointConventionBuilderExtensionsTest : Test
    {
        public EndpointConventionBuilderExtensionsTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Produces_WithTResponse_ShouldAddProducesResponseTypeMetadata()
        {
            var delegateEndpointBuilder = new FakeEndpointConventionBuilder();

            delegateEndpointBuilder.Produces<string>(StatusCodes.Status201Created, "application/json", "text/plain");

            var endpointBuilder = new RouteEndpointBuilder(context => System.Threading.Tasks.Task.CompletedTask, RoutePatternFactory.Parse("/"), 0);
            foreach (var convention in delegateEndpointBuilder.Conventions)
            {
                convention(endpointBuilder);
            }

            var typeMetadata = endpointBuilder.Metadata.OfType<IProducesResponseTypeMetadata>().FirstOrDefault();

            Assert.NotNull(typeMetadata);
            Assert.Equal(StatusCodes.Status201Created, typeMetadata.StatusCode);
            Assert.Equal(typeof(string), typeMetadata.Type);
            Assert.Contains("application/json", typeMetadata.ContentTypes);
            Assert.Contains("text/plain", typeMetadata.ContentTypes);
        }

        [Fact]
        public void Produces_WithTResponse_DefaultStatusCode_ShouldAddProducesResponseTypeMetadata()
        {
            var delegateEndpointBuilder = new FakeEndpointConventionBuilder();

            delegateEndpointBuilder.Produces<int>(contentTypes: "application/xml");

            var endpointBuilder = new RouteEndpointBuilder(context => System.Threading.Tasks.Task.CompletedTask, RoutePatternFactory.Parse("/"), 0);
            foreach (var convention in delegateEndpointBuilder.Conventions)
            {
                convention(endpointBuilder);
            }

            var typeMetadata = endpointBuilder.Metadata.OfType<IProducesResponseTypeMetadata>().FirstOrDefault();

            Assert.NotNull(typeMetadata);
            Assert.Equal(StatusCodes.Status200OK, typeMetadata.StatusCode);
            Assert.Equal(typeof(int), typeMetadata.Type);
            Assert.Contains("application/xml", typeMetadata.ContentTypes);
        }

        [Fact]
        public void Produces_WithStatusCode_ShouldAddProducesResponseTypeMetadata()
        {
            var delegateEndpointBuilder = new FakeEndpointConventionBuilder();
            
            delegateEndpointBuilder.Produces(StatusCodes.Status404NotFound);

            var endpointBuilder = new RouteEndpointBuilder(context => System.Threading.Tasks.Task.CompletedTask, RoutePatternFactory.Parse("/"), 0);
            foreach (var convention in delegateEndpointBuilder.Conventions)
            {
                convention(endpointBuilder);
            }

            var typeMetadata = endpointBuilder.Metadata.OfType<IProducesResponseTypeMetadata>().FirstOrDefault();

            Assert.NotNull(typeMetadata);
            Assert.Equal(StatusCodes.Status404NotFound, typeMetadata.StatusCode);
            Assert.Null(typeMetadata.Type);
            // ContentTypes might be empty
            Assert.Empty(typeMetadata.ContentTypes);
        }
        
        private class FakeEndpointConventionBuilder : IEndpointConventionBuilder
        {
            public List<Action<EndpointBuilder>> Conventions { get; } = [];

            public void Add(Action<EndpointBuilder> convention)
            {
                Conventions.Add(convention);
            }
        }
    }
}
