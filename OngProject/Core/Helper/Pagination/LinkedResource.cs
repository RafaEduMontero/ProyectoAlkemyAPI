namespace OngProject.Core.Helper.Pagination
{
    public record LinkedResource(string Href);
    public enum LinkedResourceType
    {
        None,
        Prev,
        Next
    }
}