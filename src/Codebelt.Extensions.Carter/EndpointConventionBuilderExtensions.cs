using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Codebelt.Extensions.Carter;

/// <summary>
/// Extension methods for <see cref="IEndpointConventionBuilder"/>.
/// </summary>
public static class EndpointConventionBuilderExtensions
{
    /// <param name="builder">The <see cref="IEndpointConventionBuilder"/> to add the metadata to.</param>
    extension(IEndpointConventionBuilder builder)
    {
        /// <summary>
        /// Adds metadata indicating that the endpoint produces a response of the specified type with the given status code and content types.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response body.</typeparam>
        /// <param name="statusCode">The HTTP status code of the response. Defaults to <see cref="StatusCodes.Status200OK"/>.</param>
        /// <param name="contentTypes">The content types produced by the endpoint.</param>
        /// <returns>The <see cref="IEndpointConventionBuilder"/> with the added metadata.</returns>
        public IEndpointConventionBuilder Produces<TResponse>(int statusCode = StatusCodes.Status200OK, params string[] contentTypes)
        {
            return builder.WithMetadata(new ProducesResponseTypeMetadata(statusCode, typeof(TResponse), contentTypes));
        }

        /// <summary>
        /// Adds metadata indicating that the endpoint produces a response with the given status code and no response body.
        /// </summary>
        /// <param name="statusCode">The HTTP status code of the response.</param>
        /// <returns>The <see cref="IEndpointConventionBuilder"/> with the added metadata.</returns>
        public IEndpointConventionBuilder Produces(int statusCode)
        {
            return builder.WithMetadata(new ProducesResponseTypeMetadata(statusCode));
        }
    }
}
