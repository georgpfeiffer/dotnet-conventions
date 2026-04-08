namespace DotnetConventions.Models;

internal enum ApplyKind { Created, Updated, NoChange, Error }

internal sealed record ApplyResult(ApplyKind Kind, string File, string Path);
