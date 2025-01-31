namespace PB202_Pronia.Services;

public static class FileService
{
    public static bool CheckTypes(this IFormFile file, params string[] types)
    {
        foreach (var type in types)
        {
            if (file.ContentType.Contains(type))
                return true;
        }

        return false;
    }

    public static bool CheckSize(this IFormFile file, int size)
    {
        return file.Length < size * 1024 * 1024;
    }

    public static string CreateImage(this IFormFile file, params string[] folders)
    {
        string path = "";

        foreach (var folder in folders)
        {
            path = Path.Combine(path, folder);
        }

        string filename = Guid.NewGuid().ToString() + file.FileName;

        path = Path.Combine(path, filename);

        using (FileStream stream = new(path, FileMode.CreateNew))
        {
            file.CopyTo(stream);
        }

        return filename;

    }

    public static bool FileDelete(string fileName, params string[] folders)
    {

        string path = "";

        foreach (var folder in folders)
        {
            path = Path.Combine(path, folder);

        }

        path = Path.Combine(path, fileName);

        if (File.Exists(path))
        {
            File.Delete(path);
            return true;
        }

        return false;
    }
}
