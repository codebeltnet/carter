using Codebelt.Extensions.YamlDotNet.Formatters;
using Microsoft.Extensions.Options;
using System.Text;
using Codebelt.Extensions.Carter.Response;
using Cuemon.Runtime.Serialization.Formatters;

namespace Codebelt.Extensions.Carter.AspNetCore.Text.Yaml;

/// <summary>
/// Provides a YAML response negotiator for Carter, capable of serializing response models to YAML format.
/// </summary>
/// <seealso cref="ConfigurableResponseNegotiator{TOptions}"/>
public class YamlResponseNegotiator : ConfigurableResponseNegotiator<YamlFormatterOptions>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="YamlResponseNegotiator"/> class.
    /// </summary>
    /// <param name="options">The <see cref="YamlFormatterOptions"/> used to configure YAML serialization and supported media types.</param>
    public YamlResponseNegotiator(IOptions<YamlFormatterOptions> options) : base(options.Value)
    {
    }

    /// <summary>
    /// Returns the character encoding specified by the <see cref="YamlFormatterOptions"/>.
    /// </summary>
    /// <returns>The default <see cref="Encoding"/> for this negotiator.</returns>
    protected override Encoding GetDefaultEncoding() => Options.Encoding;

    /// <summary>
    /// Returns a new <see cref="YamlFormatter"/> configured with the current <see cref="YamlFormatterOptions"/>.
    /// </summary>
    /// <returns>A <see cref="YamlFormatter"/> instance configured with the current <see cref="YamlFormatterOptions"/>.</returns>
    public override StreamFormatter<YamlFormatterOptions> GetFormatter()
    {
        return new YamlFormatter(Options);
    }
}
