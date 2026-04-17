# Changelog

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

For more details, please refer to `PackageReleaseNotes.txt` on a per assembly basis in the `.nuget` folder.

## [1.0.2] - 2026-04-18

This is a service update that focuses on package dependencies.

## [1.0.1] - 2026-03-25

This is a patch release focusing on dependency updates across all packages to maintain compatibility and security with the latest compatible versions.

### Changed

- Codebelt.Extensions.AspNetCore.Newtonsoft.Json upgraded to 10.1.1,
- Codebelt.Extensions.AspNetCore.Text.Yaml upgraded to 10.1.1,
- Codebelt.Extensions.Xunit.App upgraded to 11.0.8,
- Cuemon.Core upgraded to 10.5.0,
- Cuemon.Extensions.AspNetCore.Text.Json upgraded to 10.5.0,
- Cuemon.Extensions.AspNetCore.Xml upgraded to 10.5.0,
- Cuemon.Extensions.IO upgraded to 10.5.0,
- coverlet.collector upgraded to 8.0.1,
- coverlet.msbuild upgraded to 8.0.1.

## [1.0.0] - 2026-03-01

This is the initial stable release of the `Codebelt.Extensions.Carter`, `Codebelt.Extensions.Carter.AspNetCore.Newtonsoft.Json`, `Codebelt.Extensions.Carter.AspNetCore.Text.Json`, `Codebelt.Extensions.Carter.AspNetCore.Text.Yaml` and `Codebelt.Extensions.Carter.AspNetCore.Xml` packages.

### Added

- `ConfigurableResponseNegotiator<TOptions>` class in the Codebelt.Extensions.Carter.Response namespace that provides an abstract, configurable base class for Carter response negotiators that serialize models using a `StreamFormatter<TOptions>` implementation,
- `EndpointConventionBuilderExtensions` class in the Codebelt.Extensions.Carter namespace that consist of extension methods for the `IEndpointConventionBuilder` interface: `Produces<TResponse>` and `Produces`,
- `NewtonsoftJsonNegotiator` class in the Codebelt.Extensions.Carter.AspNetCore.Newtonsoft.Json namespace that provides a JSON response negotiator for Carter, capable of serializing response models to JSON format using `Newtonsoft.Json`,
- `JsonResponseNegotiator` class in the Codebelt.Extensions.Carter.AspNetCore.Text.Json namespace that provides a JSON response negotiator for Carter, capable of serializing response models to JSON format using `System.Text.Json`,
- `YamlResponseNegotiator` class in the Codebelt.Extensions.Carter.AspNetCore.Text.Yaml namespace that provides a YAML response negotiator for Carter, capable of serializing response models to YAML format using `YamlDotNet`,
- `XmlResponseNegotiator` class in the Codebelt.Extensions.Carter.AspNetCore.Xml namespace that provides an XML response negotiator for Carter, capable of serializing response models to XML format using `System.Xml.XmlWriter`.

[Unreleased]: https://github.com/codebeltnet/carter/compare/v1.0.1...HEAD
[1.0.1]: https://github.com/codebeltnet/carter/compare/v1.0.0...v1.0.1
[1.0.0]: https://github.com/codebeltnet/carter/releases/tag/v1.0.0
