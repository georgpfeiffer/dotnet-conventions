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

## Local development

From the repository root:

```bash
dotnet run --project src/DotnetConventions -- apply
dotnet run --project src/DotnetConventions -- verify
```

Pack locally:

```bash
dotnet pack src/DotnetConventions -c Release -o ./artifacts
```

## License

MIT — see [LICENSE](LICENSE).
