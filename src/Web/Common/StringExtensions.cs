namespace EssayFeedback;

public static class StringExtensions
{
    public static string GetBetween(this string text, string start, string end)
    {
        var startIndex = text.IndexOf(start, StringComparison.Ordinal);
        if (startIndex < 0) return string.Empty;
        
        startIndex += start.Length;
        var endIndex = text.IndexOf(end, startIndex, StringComparison.Ordinal);
        if (endIndex < 0) return string.Empty;
        
        return text[startIndex..endIndex];
    }
}