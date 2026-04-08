namespace DotnetConventions.Helpers;

public static class HashHelper
{
    public static async Task<string> ComputeFileHashAsync(string filePath)
    {
        using var sha = System.Security.Cryptography.SHA256.Create();
        await using var fs = File.OpenRead(filePath);
        var hash = await sha.ComputeHashAsync(fs);
        return Convert.ToHexString(hash);
    }

    public static string ComputeByteHash(byte[] data)
    {
        var hash = System.Security.Cryptography.SHA256.HashData(data);
        return Convert.ToHexString(hash);
    }
}
