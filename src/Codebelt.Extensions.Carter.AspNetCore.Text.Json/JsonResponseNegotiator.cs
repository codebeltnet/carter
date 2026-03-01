using System.Text;
using Codebelt.Extensions.Carter.Response;
using Cuemon.Extensions.Text.Json.Formatters;
using Cuemon.Runtime.Serialization.Formatters;
using Microsoft.Extensions.Options;

namespace Codebelt.Extensions.Carter.AspNetCore.Text.Json;

/// <summary>
/// Provides a JSON response negotiator for Carter, capable of serializing response models to JSON format using System.Text.Json.
/// </summary>
/// <seealso cref="ConfigurableResponseNegotiator{TOptions}"/>
public class JsonResponseNegotiator : ConfigurableResponseNegotiator<JsonFormatterOptions>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JsonResponseNegotiator"/> class.
    /// </summary>
    /// <param name="options">The <see cref="JsonFormatterOptions"/> used to configure JSON serialization and supported media types.</param>
    public JsonResponseNegotiator(IOptions<JsonFormatterOptions> options) : base(options.Value)
    {
    }

    /// <summary>
    /// Returns UTF-8 without a byte-order mark (BOM) as the default character encoding for this negotiator.
    /// </summary>
    /// <returns>A <see cref="UTF8Encoding"/> instance with the byte-order mark disabled.</returns>
    protected override Encoding GetDefaultEncoding()
    {
        return new UTF8Encoding(false);
    }

    /// <summary>
    /// Returns a new <see cref="JsonFormatter"/> configured with the current <see cref="JsonFormatterOptions"/>.
    /// </summary>
    /// <returns>A <see cref="JsonFormatter"/> instance configured with the current <see cref="JsonFormatterOptions"/>.</returns>
    public override StreamFormatter<JsonFormatterOptions> GetFormatter()
    {
        return new JsonFormatter(Options);
    }
}
