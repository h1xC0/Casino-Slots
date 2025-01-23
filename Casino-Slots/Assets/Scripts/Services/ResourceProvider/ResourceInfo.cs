public class ResourceInfo
{
    public string Path;
    public System.Type Type;

    public ResourceInfo(System.Type type, string path)
    {
        Type = type;
        Path = path;
    }
}