# Changelog

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

For more details, please refer to `PackageReleaseNotes.txt` on a per assembly basis in the `.nuget` folder.

## [1.0.0] - 2026-03-02

This is the initial stable release of the `Codebelt.Extensions.Carter`, `Codebelt.Extensions.Carter.AspNetCore.Newtonsoft.Json`, `Codebelt.Extensions.Carter.AspNetCore.Text.Json`, `Codebelt.Extensions.Carter.AspNetCore.Text.Yaml` and `Codebelt.Extensions.Carter.AspNetCore.Xml` packages.

### Added

- `ConfigurableResponseNegotiator<TOptions>` class in the Codebelt.Extensions.Carter.Response namespace that provides an abstract, configurable base class for Carter response negotiators that serialize models using a `StreamFormatter<TOptions>` implementation,
- `EndpointConventionBuilderExtensions` class in the Codebelt.Extensions.Carter namespace that consist of extension methods for the `IEndpointConventionBuilder` interface: `Produces<TResponse>` and `Produces`,
- `NewtonsoftJsonNegotiator` class in the Codebelt.Extensions.Carter.AspNetCore.Newtonsoft.Json namespace that provides a JSON response negotiator for Carter, capable of serializing response models to JSON format using `Newtonsoft.Json`,
- `JsonResponseNegotiator` class in the Codebelt.Extensions.Carter.AspNetCore.Text.Json namespace that provides a JSON response negotiator for Carter, capable of serializing response models to JSON format using `System.Text.Json`,
- `YamlResponseNegotiator` class in the Codebelt.Extensions.Carter.AspNetCore.Text.Yaml namespace that provides a YAML response negotiator for Carter, capable of serializing response models to YAML format using `YamlDotNet`,
- `XmlResponseNegotiator` class in the Codebelt.Extensions.Carter.AspNetCore.Xml namespace that provides an XML response negotiator for Carter, capable of serializing response models to XML format using `System.Xml.XmlWriter`.
