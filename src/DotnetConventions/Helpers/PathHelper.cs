namespace DotnetConventions.Helpers;

public static class PathHelper
{
    public static string GetRepositoryRootPath()
    {
        var from = Directory.GetCurrentDirectory();
        var dir = new DirectoryInfo(from);

        while (dir is not null)
        {
            if (Directory.Exists(Path.Combine(dir.FullName, ".git")))
            {
                return dir.FullName;
            }

            dir = dir.Parent;
        }

        return Directory.GetCurrentDirectory();
    }
}
