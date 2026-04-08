namespace DotnetConventions.Models;

internal enum VerifyKind { Match, Missing, Different, Error }

internal sealed record VerifyResult(VerifyKind Kind, string File, string Path, string? Message = null);
