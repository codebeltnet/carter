using Codebelt.Extensions.Carter.Response;
using Cuemon.Xml.Serialization.Formatters;
using Microsoft.Extensions.Options;
using System.Text;
using Cuemon.Runtime.Serialization.Formatters;

namespace Codebelt.Extensions.Carter.AspNetCore.Xml
{
    /// <summary>
    /// Provides an XML response negotiator for Carter, capable of serializing response models to XML format.
    /// </summary>
    /// <seealso cref="ConfigurableResponseNegotiator{TOptions}"/>
        public class XmlResponseNegotiator : ConfigurableResponseNegotiator<XmlFormatterOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlResponseNegotiator"/> class.
        /// </summary>
        /// <param name="options">The <see cref="XmlFormatterOptions"/> used to configure XML serialization and supported media types.</param>
        public XmlResponseNegotiator(IOptions<XmlFormatterOptions> options) : base(options.Value)
        {
        }

        /// <summary>
        /// Returns the character encoding specified by the <see cref="XmlFormatterOptions"/> writer settings.
        /// </summary>
        /// <returns>The default <see cref="Encoding"/> for this negotiator.</returns>
        protected override Encoding GetDefaultEncoding() => Options.Settings.Writer.Encoding;

        /// <summary>
        /// Returns a new <see cref="XmlFormatter"/> configured with the current <see cref="XmlFormatterOptions"/>.
        /// </summary>
        /// <returns>A <see cref="XmlFormatter"/> instance configured with the current <see cref="XmlFormatterOptions"/>.</returns>
        public override StreamFormatter<XmlFormatterOptions> GetFormatter()
        {
            return new XmlFormatter(Options);
        }
    }
}
