using System.Reflection;

namespace DotnetConventions.Helpers;

public static class FileHelper
{
    public static async Task<byte[]> ReadEmbeddedAsync(string logicalName)
    {
        await using var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(logicalName) ??
                                         throw new InvalidOperationException($"Missing embedded resource: {logicalName}");
        using var ms = new MemoryStream();
        await resourceStream.CopyToAsync(ms);
        return ms.ToArray();
    }
}
