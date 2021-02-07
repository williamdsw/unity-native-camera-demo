
public class StringUtils
{
    public static string GetMediaName(string path)
    {
        if (string.IsNullOrEmpty(path) || string.IsNullOrWhiteSpace(path))
        {
            return "Path is invalid!";
        }

        int indexOf = path.LastIndexOf('/');
        if (indexOf != -1)
        {
            path = path.Substring(indexOf + 1);
        }

        indexOf = path.LastIndexOf('\\');
        if (indexOf != -1)
        {
            path = path.Substring(indexOf + 1);
        }

        return path;
    }
}
