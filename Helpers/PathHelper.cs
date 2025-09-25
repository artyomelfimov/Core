namespace Core.Helpers;

public class PathHelper
{
    public static string GetRootFolderFromPath(string relativePath)
    {
        while (true)
        {
            var directoryName = Path.GetDirectoryName(relativePath);
            if (!string.IsNullOrEmpty(directoryName))
                relativePath = directoryName;
            else
                break;
        }
        return relativePath;
    }
}
