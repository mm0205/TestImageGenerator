using System.IO.Abstractions;

namespace Mm0205.TestImageGenerator.Cli.Util;

public static class PathExtension
{
    public static bool TryGetFullPath(this IPath pathObject, string originalPath, out string fullPath)
    {
        try
        {
            fullPath = pathObject.GetFullPath(originalPath);
            return true;
        }
        catch
        {
            fullPath = string.Empty;
            return false;
        }
    }
}