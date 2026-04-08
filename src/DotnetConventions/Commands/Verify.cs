using DotnetConventions.Helpers;
using DotnetConventions.Models;
using File = System.IO.File;
using Path = System.IO.Path;

namespace DotnetConventions.Commands;

public static class Verify
{
    public static async Task Handle(Dictionary<string, string> embeddedFiles)
    {
        var repositoryRootPath = PathHelper.GetRepositoryRootPath();

        var results = new List<VerifyResult>();
        foreach (var (fileName, resourceName) in embeddedFiles)
        {
            results.Add(await VerifyFile(repositoryRootPath, fileName, resourceName));
        }

        foreach (var r in results)
        {
            Console.WriteLine($"{r.Kind.ToString().ToUpperInvariant(),-10} {r.File} -> {r.Path}");
            if (!string.IsNullOrWhiteSpace(r.Message))
            {
                Console.WriteLine($"           {r.Message}");
            }
        }

        var allMatching = results.TrueForAll(r => r.Kind == VerifyKind.Match);
        Environment.ExitCode = allMatching ? 0 : 1;
    }

    private static async Task<VerifyResult> VerifyFile(string rootRepositoryPath, string fileName, string resourceName)
    {
        try
        {
            var destinationFilePath = Path.Combine(rootRepositoryPath, fileName);

            if (!File.Exists(destinationFilePath))
            {
                return new(VerifyKind.Missing, fileName, destinationFilePath, "File does not exist.");
            }

            var embeddedFileContents = await FileHelper.ReadEmbeddedAsync(resourceName);
            var currentFileHash = await HashHelper.ComputeFileHashAsync(destinationFilePath);
            var embeddedHash = HashHelper.ComputeByteHash(embeddedFileContents);

            if (currentFileHash == embeddedHash)
            {
                return new(VerifyKind.Match, fileName, destinationFilePath);
            }

            return new(VerifyKind.Different, fileName, destinationFilePath, "Contents differ from embedded template.");
        }
        catch (Exception ex)
        {
            return new(VerifyKind.Error, fileName, rootRepositoryPath, $"{ex.GetType().Name}: {ex.Message}");
        }
    }
}
