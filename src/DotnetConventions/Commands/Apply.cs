using DotnetConventions.Helpers;
using DotnetConventions.Models;
using File = System.IO.File;
using Path = System.IO.Path;

namespace DotnetConventions.Commands;

public static class Apply
{
    public static async Task Handle(Dictionary<string, string> embeddedFiles)
    {
        var repositoryRootPath = PathHelper.GetRepositoryRootPath();

        var results = new List<ApplyResult>();
        foreach (var (fileName, resourceName) in embeddedFiles)
        {
            results.Add(await ApplyFile(repositoryRootPath, fileName, resourceName));
        }

        foreach (var r in results)
        {
            Console.WriteLine($"{r.Kind,-16} {r.File} -> {r.Path}");
        }
    }

    private static async Task<ApplyResult> ApplyFile(string rootRepositoryPath, string fileName, string resourceName)
    {
        try
        {
            var destinationFilePath = Path.Combine(rootRepositoryPath, fileName);
            var embeddedFileContents = await FileHelper.ReadEmbeddedAsync(resourceName);

            if (!File.Exists(destinationFilePath))
            {
                await File.WriteAllBytesAsync(destinationFilePath, embeddedFileContents);
                return new(ApplyKind.Created, fileName, destinationFilePath);
            }

            var currentFileHash = await HashHelper.ComputeFileHashAsync(destinationFilePath);
            var embeddedHash = HashHelper.ComputeByteHash(embeddedFileContents);

            if (currentFileHash == embeddedHash)
            {
                return new(ApplyKind.NoChange, fileName, destinationFilePath);
            }

            await File.WriteAllBytesAsync(destinationFilePath, embeddedFileContents);
            return new(ApplyKind.Updated, fileName, destinationFilePath);
        }
        catch (Exception ex)
        {
            return new(ApplyKind.Error, fileName, $"{ex.GetType().Name}: {ex.Message}");
        }
    }
}
