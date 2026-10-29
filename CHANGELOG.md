# Changelog

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

For more details, please refer to `PackageReleaseNotes.txt` on a per assembly basis in the `.nuget` folder.

## [1.1.0] - 2026-11-10

This is a minor release that adds .NET 11 as a supported target framework alongside .NET 10 for all packages, with matching build, CI, and documentation updates. Source and test changes are formatting only, and the release also upgrades the Codebelt.Coverlet.MTP test coverage tooling.

### Changed

- Added .NET 11 (`net11.0`) as a target framework alongside .NET 10 for the Codebelt.Extensions.Carter packages, with shared build properties also targeting `net11.0` for the source, test, and benchmark projects,
- Updated "Codebelt.Coverlet.MTP" from 10.1.0 to 10.1.1,
- Documented .NET 11 support in the repository README, AGENTS guidance, contributing guide, availability include, and package READMEs,
- Moved CI runners to Ubuntu 26.04 and enabled preview SDKs so .NET 11 can be built and tested before its final release.

## [1.0.9] - 2026-10-17

This is a patch release updating dependencies, documentation hosting, and repository agent guidance.

### Added

- Added "Codebelt.Coverlet.MTP" 10.1.0 for MTP coverage and "Microsoft.Testing.Extensions.HangDump" 2.5.1 for hang diagnostics,
- Added an nginx configuration serving documentation on port 8080, caching fingerprinted assets, and disabling caching for stable URLs.

### Changed

- Updated "Codebelt.Extensions.AspNetCore.Newtonsoft.Json" from 10.1.8 to 10.1.9,
- Updated "Codebelt.Extensions.AspNetCore.Text.Yaml" from 10.1.8 to 10.1.9,
- Updated "Codebelt.Extensions.Xunit.App" from 12.0.1 to 12.0.2,
- Updated "Cuemon.Core" from 10.7.1 to 10.8.1,
- Updated "Cuemon.Extensions.AspNetCore.Text.Json" from 10.7.1 to 10.8.1,
- Updated "Cuemon.Extensions.AspNetCore.Xml" from 10.7.1 to 10.8.1,
- Updated "Cuemon.Extensions.IO" from 10.7.1 to 10.8.1,
- Updated "Microsoft.NET.Test.Sdk" from 18.10.0 to 18.11.0,
- Updated "xunit.v3" from 4.0.1 to 4.0.2,
- Updated "xunit.v3.runner.console" from 4.0.1 to 4.0.2,
- Updated "xunit.runner.visualstudio" from 4.0.0 to 4.0.1,
- Updated the DocFX container to use dhi.io/nginx and codebeltnet/docfx:2, with separate API member pages, the ms-style template, and a repository-specific sitemap,
- Consolidated the full test, benchmark, and XML documentation guidance from .github/copilot-instructions.md into AGENTS.md with explicit path scopes, and updated guidance references.

### Removed

- Removed the direct "Microsoft.Testing.Extensions.CodeCoverage" test dependency in favor of "Codebelt.Coverlet.MTP".

## [1.0.8] - 2026-09-13

This is a patch release focused on modernizing test infrastructure, migrating code coverage tooling to the Microsoft Testing Platform ecosystem, and updating shared Codebelt dependencies to their latest stable versions.

### Changed

- Codebelt.Extensions.AspNetCore.Newtonsoft.Json upgraded to 10.1.8,
- Codebelt.Extensions.AspNetCore.Text.Yaml upgraded to 10.1.8,
- Codebelt.Extensions.Xunit.App upgraded to 12.0.1,
- Cuemon.Core upgraded to 10.7.1,
- Cuemon.Extensions.AspNetCore.Text.Json upgraded to 10.7.1,
- Cuemon.Extensions.AspNetCore.Xml upgraded to 10.7.1,
- Cuemon.Extensions.IO upgraded to 10.7.1,
- Microsoft.NET.Test.Sdk upgraded to 18.10.0,
- MinVer upgraded to 8.0.0,
- xunit.v3 upgraded to 4.0.0,
- xunit.v3.runner.console upgraded to 4.0.0,
- xunit.runner.visualstudio upgraded to 4.0.0,
- Code coverage tooling migrated from Coverlet (coverlet.msbuild and coverlet.collector) to Microsoft.Testing.Extensions.CodeCoverage for improved test platform integration,
- Test runner configuration added to global.json to use Microsoft.Testing.Platform as the test orchestration platform,
- Contributing guidelines significantly expanded to document Codebelt .NET library estate standards, repository structure, build and test processes, and development expectations.

### Removed

- Removed `coverlet.collector` and `coverlet.msbuild` from test project dependencies.

## [1.0.7] - 2026-08-16

This is a patch release focused on updating package dependencies to their latest compatible versions and refreshing repository configuration and test environment setup.

### Changed

- Codebelt.Extensions.Xunit.App upgraded to 11.2.1,
- Codebelt.Extensions.AspNetCore.Newtonsoft.Json upgraded to 10.1.7,
- Cuemon.Core upgraded to 10.7.0,
- Cuemon.Extensions.AspNetCore.Text.Json upgraded to 10.7.0,
- Cuemon.Extensions.AspNetCore.Xml upgraded to 10.7.0,
- Cuemon.Extensions.IO upgraded to 10.7.0,
- Microsoft.NET.Test.Sdk upgraded to 18.9.0,
- Repository configuration updated: removed obsolete diagnostic rule suppressions from .editorconfig and added standard .gitattributes for line-ending normalization and merge handling,
- Docker test environment image updated to support multi-version testing across .NET 8, 9, 10, and 11.

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

[1.1.0]: https://github.com/codebeltnet/carter/compare/v1.0.9...v1.1.0
[1.0.9]: https://github.com/codebeltnet/carter/compare/v1.0.8...v1.0.9
[1.0.8]: https://github.com/codebeltnet/carter/compare/v1.0.7...v1.0.8
[1.0.7]: https://github.com/codebeltnet/carter/compare/v1.0.6...v1.0.7
[1.0.6]: https://github.com/codebeltnet/carter/compare/v1.0.5...v1.0.6
[1.0.5]: https://github.com/codebeltnet/carter/compare/v1.0.4...v1.0.5
[1.0.4]: https://github.com/codebeltnet/carter/compare/v1.0.3...v1.0.4
[1.0.3]: https://github.com/codebeltnet/carter/compare/v1.0.2...v1.0.3
[1.0.2]: https://github.com/codebeltnet/carter/compare/v1.0.1...v1.0.2
[1.0.1]: https://github.com/codebeltnet/carter/compare/v1.0.0...v1.0.1
[1.0.0]: https://github.com/codebeltnet/carter/releases/tag/v1.0.0
