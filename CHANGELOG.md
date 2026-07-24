# Changelog

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

For more details, please refer to `PackageReleaseNotes.txt` on a per assembly basis in the `.nuget` folder.

## [1.0.6] - 2026-07-24

This is a patch release that updates Cuemon and Codebelt shared dependencies to their latest stable versions, improves build configuration, enhances CI/CD automation, and modernizes code formatting.

### Changed

- Codebelt.Extensions.AspNetCore.Newtonsoft.Json upgraded to 10.1.6,
- Codebelt.Extensions.AspNetCore.Text.Yaml upgraded to 10.1.6,
- Codebelt.Extensions.Xunit.App upgraded to 11.1.2,
- Cuemon.Core upgraded to 10.5.5,
- Cuemon.Extensions.AspNetCore.Text.Json upgraded to 10.5.5,
- Cuemon.Extensions.AspNetCore.Xml upgraded to 10.5.5,
- Cuemon.Extensions.IO upgraded to 10.5.5,
- Microsoft.NET.Test.Sdk upgraded to 18.8.1,
- Enhanced the NuGet package bumping script with intelligent version management, NuGet API lookups with caching, and improved output messaging,
- Refreshed the DocFX site container's NGINX base image to 1.31-alpine to allow more flexibility in the specific NGINX release,
- Modernized XmlResponseNegotiator code formatting by adopting file-scoped namespace syntax.

### Fixed

- Corrected the AnalysisMode property name in Directory.Build.props to use the correct MSBuild property.

## [1.0.5] - 2026-07-01

This is a patch service update that refreshes shared package baselines, hardens the DocFX publishing pipeline with new per-type usage examples, and tightens CI deployment gating so skipped optional jobs no longer suppress package publishing.

### Added

- Per-type DocFX overwrite pages with usage examples for `NewtonsoftJsonNegotiator`, `JsonResponseNegotiator`, `YamlResponseNegotiator`, `XmlResponseNegotiator`, and `EndpointConventionBuilderExtensions`,
- `Codebelt.Extensions.Carter.Response` namespace overview page that consolidates guidance on the `ConfigurableResponseNegotiator<TOptions>` base class.

### Changed

- `Codebelt.Extensions.AspNetCore.Newtonsoft.Json` upgraded to 10.1.5,
- `Codebelt.Extensions.AspNetCore.Text.Yaml` upgraded to 10.1.5,
- `Codebelt.Extensions.Xunit.App` upgraded to 11.1.1,
- `Cuemon.Core` upgraded to 10.5.4,
- `Cuemon.Extensions.AspNetCore.Text.Json` upgraded to 10.5.4,
- `Cuemon.Extensions.AspNetCore.Xml` upgraded to 10.5.4,
- `Cuemon.Extensions.IO` upgraded to 10.5.4,
- `Microsoft.NET.Test.Sdk` upgraded to 18.7.0,
- Hardened the DocFX publishing config so namespace and type overwrite inputs are kept in separate subdirectories and excluded from conceptual content,
- Refreshed the DocFX site container's NGINX base image to 1.31.2-alpine,
- Codified the repository's DocFX authoring requirements, verification gates, and temp-artifact handling in `AGENTS.md`,
- Guarded the deploy job so skipped optional jobs (such as disabled macOS matrix runs) no longer suppress package publishing when the required build, test, and quality gates succeed.

## [1.0.4] - 2026-06-06

This is a service update that focuses on package dependencies.

## [1.0.3] - 2026-05-23

This is a service update that focuses on package dependencies.

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

[1.0.6]: https://github.com/codebeltnet/carter/compare/v1.0.5...v1.0.6
[1.0.5]: https://github.com/codebeltnet/carter/compare/v1.0.4...v1.0.5
[1.0.4]: https://github.com/codebeltnet/carter/compare/v1.0.3...v1.0.4
[1.0.3]: https://github.com/codebeltnet/carter/compare/v1.0.2...v1.0.3
[1.0.2]: https://github.com/codebeltnet/carter/compare/v1.0.1...v1.0.2
[1.0.1]: https://github.com/codebeltnet/carter/compare/v1.0.0...v1.0.1
[1.0.0]: https://github.com/codebeltnet/carter/releases/tag/v1.0.0
