namespace SigmaInsight.Web;

public static class EnumerableExtensions
{
    public static string AsConcatenatedString(this IEnumerable<string> source, string separator)
    {
        return string.Join(separator, source);
    }
}