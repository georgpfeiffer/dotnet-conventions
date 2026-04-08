# dotnet-conventions

A lightweight **.NET global tool** for applying shared conventions — `.editorconfig`, `Directory.Build.props` and `Directory.Build.targets` — to any .NET repository.

## Overview

Keeping consistent **code style** and **build settings** across many repositories is tedious and error-prone. This tool makes it a one-liner:

```bash
dotnet-conventions apply
```

It walks up from the current directory to find the repository root (by looking for `.git`), then creates or updates:

- `.editorconfig`
- `Directory.Build.props`
- `Directory.Build.targets`

so all projects in the repo share the same conventions.

## Commands

| Command  | Description                                                                                                     |
|----------|-----------------------------------------------------------------------------------------------------------------|
| `apply`  | Creates or updates `.editorconfig`, `Directory.Build.props` and `Directory.Build.targets` in the repo root.     |
| `verify` | Verifies the files match the shipped templates. Exits `0` on match, `1` on any mismatch. Suitable for CI.       |

## Installation

Install globally from NuGet:

```bash
dotnet tool install -g dotnet-conventions
```

Then use it anywhere:

```bash
dotnet-conventions apply
dotnet-conventions verify
```

To update to the latest version:

```bash
dotnet tool update -g dotnet-conventions
```

## Versioning

Package versions are calculated automatically by [GitVersion](https://gitversion.net/) from git history and tags — there is no `<Version>` property in the csproj and no manual version parameter anywhere.

- **Release versions** (e.g. `1.5.0`) are published from `main` or `release/*` branches.
- **Prerelease versions** (e.g. `1.5.0-my-feature.3`) are published from any other branch; the branch name becomes the prerelease label.
- **Version bumps** default to patch. Include `+semver: minor` or `+semver: major` in a commit message (or a squash-merge PR title) to bump minor or major instead.

The rules live in [`GitVersion.yml`](GitVersion.yml). `gitversion.tool` is pinned in [`.config/dotnet-tools.json`](.config/dotnet-tools.json) and restored via `dotnet tool restore`. The [`nuget-publish`](.github/workflows/nuget-publish.yml) workflow runs GitVersion, passes the computed version into `dotnet pack` via `-p:Version=...`, publishes to nuget.org, and finally creates and pushes the matching git tag.

## Local development

From the repository root:

```bash
dotnet tool restore
dotnet dotnet-gitversion            # prints the calculated version for your current branch
dotnet run --project src/DotnetConventions -- apply
dotnet run --project src/DotnetConventions -- verify
```

Pack locally with a GitVersion-derived version:

```bash
SEM_VERSION=$(dotnet dotnet-gitversion /showvariable SemVer)
dotnet pack src/DotnetConventions -c Release -p:Version=$SEM_VERSION -o ./artifacts
```

## License

MIT — see [LICENSE](LICENSE).
