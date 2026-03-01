using Carter;
using System;
using System.Linq;
using Cuemon.Configuration;
using Cuemon.Diagnostics;
using Cuemon.Net.Http;
using Cuemon.Runtime.Serialization.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cuemon;

namespace Codebelt.Extensions.Carter.Response;

/// <summary>
/// Provides an abstract, configurable base class for Carter response negotiators that serialize models using a <see cref="StreamFormatter{TOptions}"/>.
/// </summary>
/// <typeparam name="TOptions">The type of the configured options.</typeparam>
/// <seealso cref="Configurable{TOptions}"/>
/// <seealso cref="IResponseNegotiator"/>
/// <seealso cref="IContentNegotiation"/>
/// <seealso cref="IExceptionDescriptorOptions"/>
/// <seealso cref="IValidatableParameterObject"/>
public abstract class ConfigurableResponseNegotiator<TOptions> : Configurable<TOptions>, IResponseNegotiator where TOptions : class, IExceptionDescriptorOptions, IContentNegotiation, IValidatableParameterObject, new()
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigurableResponseNegotiator{TOptions}"/> class.
    /// </summary>
    /// <param name="options">The <typeparamref name="TOptions"/> used to configure content negotiation and serialization behavior.</param>
    protected ConfigurableResponseNegotiator(TOptions options) : base(options)
    {
    }

    /// <summary>
    /// Determines whether this negotiator can handle the specified <paramref name="accept"/> media type.
    /// </summary>
    /// <param name="accept">The <see cref="MediaTypeHeaderValue"/> from the HTTP request's Accept header.</param>
    /// <returns><c>true</c> if the negotiator can handle the specified media type; otherwise, <c>false</c>.</returns>
    public virtual bool CanHandle(MediaTypeHeaderValue accept)
    {
        return Options.SupportedMediaTypes.Any(mediaType =>
        {
            if (accept.MatchesMediaType(mediaType.MediaType))
            {
                ContentType = mediaType.MediaType;
                return true;
            }
            return false;
        });
    }

    /// <summary>
    /// Gets the matched content type determined by the most recent successful call to <see cref="CanHandle"/>.
    /// </summary>
    /// <value>The matched content type media type string, or <c>null</c> if <see cref="CanHandle"/> has not yet been called successfully.</value>
    public string ContentType { get; private set; }

    /// <summary>
    /// Resolves the character encoding for the HTTP response by inspecting the <c>Accept-Charset</c> header
    /// of the <paramref name="request"/>, falling back to <see cref="GetDefaultEncoding"/> when no
    /// valid charset is indicated.
    /// </summary>
    /// <param name="request">The current <see cref="HttpRequest"/>.</param>
    /// <returns>The <see cref="Encoding"/> to use when writing the response body.</returns>
    public virtual Encoding GetEncoding(HttpRequest request)
    {
        var acceptCharset = request.GetTypedHeaders().AcceptCharset;
        if (acceptCharset.Count > 0)
        {
            var preferred = acceptCharset
                .OrderByDescending(x => x.Quality ?? 1.0)
                .Select(x => x.Value.Value)
                .FirstOrDefault(charset =>
                {
                    return Patterns.TryInvoke(() => Encoding.GetEncoding(charset!));
                });

            if (preferred is not null)
            {
                return Encoding.GetEncoding(preferred);
            }
        }
        return GetDefaultEncoding();
    }

    /// <summary>
    /// When overridden in a derived class, returns the default character encoding for this negotiator.
    /// This encoding is used as the fallback by <see cref="GetEncoding"/> when the HTTP request does not
    /// specify a resolvable <c>Accept-Charset</c> preference.
    /// </summary>
    /// <returns>The default <see cref="Encoding"/> for this negotiator.</returns>
    protected abstract Encoding GetDefaultEncoding();

    /// <summary>
    /// When overridden in a derived class, returns the <see cref="StreamFormatter{TOptions}"/> used to serialize the response model.
    /// </summary>
    /// <returns>A <see cref="StreamFormatter{TOptions}"/> configured with the current <typeparamref name="TOptions"/>.</returns>
    public abstract StreamFormatter<TOptions> GetFormatter();

    /// <summary>
    /// Serializes the specified <paramref name="model"/> and writes it to the HTTP response body.
    /// </summary>
    /// <typeparam name="T">The type of the model to serialize.</typeparam>
    /// <param name="req">The current <see cref="HttpRequest"/>.</param>
    /// <param name="res">The current <see cref="HttpResponse"/> to which the serialized content is written.</param>
    /// <param name="model">The model to serialize.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to propagate notification that the operation should be canceled.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous write operation.</returns>
    public virtual async Task Handle<T>(HttpRequest req, HttpResponse res, T model, CancellationToken cancellationToken)
    {
        var encoding = GetEncoding(req);
        res.ContentType = ContentType + "; charset=" + encoding.WebName;
        await using var textWriter = new StreamWriter(res.Body, encoding, bufferSize: -1, leaveOpen: true);
        var formatter = GetFormatter();
        using (var streamReader = new StreamReader(formatter.Serialize(model), encoding))
        {
            Memory<char> memoryBuffer = new char[8192];
            int read;
            while ((read = await streamReader.ReadAsync(memoryBuffer, cancellationToken).ConfigureAwait(false)) != 0)
            {
                await textWriter.WriteAsync(memoryBuffer.Slice(0, read), cancellationToken).ConfigureAwait(false);
            }
        }
        await textWriter.FlushAsync(cancellationToken).ConfigureAwait(false);
    }
}
