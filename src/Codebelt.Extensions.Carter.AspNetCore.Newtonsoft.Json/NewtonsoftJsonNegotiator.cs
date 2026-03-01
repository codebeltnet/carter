using System.Text;
using Codebelt.Extensions.Carter.Response;
using Codebelt.Extensions.Newtonsoft.Json.Formatters;
using Cuemon.Runtime.Serialization.Formatters;
using Microsoft.Extensions.Options;

namespace Codebelt.Extensions.Carter.AspNetCore.Newtonsoft.Json;

/// <summary>
/// Provides a JSON response negotiator for Carter, capable of serializing response models to JSON format using Newtonsoft.Json.
/// </summary>
/// <seealso cref="ConfigurableResponseNegotiator{TOptions}"/>
public class NewtonsoftJsonNegotiator : ConfigurableResponseNegotiator<NewtonsoftJsonFormatterOptions>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NewtonsoftJsonNegotiator"/> class.
    /// </summary>
    /// <param name="options">The <see cref="NewtonsoftJsonFormatterOptions"/> used to configure JSON serialization and supported media types.</param>
    public NewtonsoftJsonNegotiator(IOptions<NewtonsoftJsonFormatterOptions> options) : base(options.Value)
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
    /// Returns a new <see cref="NewtonsoftJsonFormatter"/> configured with the current <see cref="NewtonsoftJsonFormatterOptions"/>.
    /// </summary>
    /// <returns>A <see cref="NewtonsoftJsonFormatter"/> instance configured with the current <see cref="NewtonsoftJsonFormatterOptions"/>.</returns>
    public override StreamFormatter<NewtonsoftJsonFormatterOptions> GetFormatter()
    {
        return new NewtonsoftJsonFormatter(Options);
    }
}
