namespace Forum.Web.Framework.Path;

public static class PathExtentions
{
    public static string RemoveQueryParams(this string path, params string[] parameters)
    {
        foreach (string param in parameters)
        {
            ArgumentException.ThrowIfNullOrEmpty(param);

            if (!path.Contains(param)) continue;

            path = RemoveQueryParam(path, param);
        }

        return path;
    }

    private static string RemoveQueryParam(string path, string param)
    {
        path = RemoveParam(path, param);
        path = RemoveLastQuestionMarkIfExists(path);
        path = RemoveLastAmpersandIfExists(path);

        return path;
    }

    private static string RemoveParam(string path, string param)
    {
        int indexOfParam = path.IndexOf(param);
        int indexOfAmpersand = path.IndexOf('&', indexOfParam);

        if (indexOfAmpersand != -1)
        {
            path = path.Remove(indexOfParam, indexOfAmpersand - indexOfParam + 1);
        }
        else
        {
            path = path.Remove(indexOfParam, path.Length - indexOfParam);
        }
        return path;
    }

    private static string RemoveLastQuestionMarkIfExists(string path)
    {
        if (path.LastIndexOf('?') == (path.Length - 1))
        {
            path = path.Remove(path.LastIndexOf('?'), 1);
        }
        return path;
    }

    private static string RemoveLastAmpersandIfExists(string path)
    {
        if (path.LastIndexOf('&') == (path.Length - 1))
        {
            path = path.Remove(path.LastIndexOf('&'), 1);
        }
        return path;
    }

    public static bool ContainsQueryParams(this string path)
    {
        return path.Contains('?');
    }
}