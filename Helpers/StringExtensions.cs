using System.Text.RegularExpressions;

namespace PurchasePortal.Web.Helpers
{
    public static class StringExtensions
    {
        public static string Slugify(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";

            text = text.ToLower()
                       .Replace(" ", "-")
                       .Replace(".", "")
                       .Replace(",", "")
                       .Replace("?", "")
                       .Replace("!", "")
                       .Replace("'", "")
                       .Replace("\"", "")
                       .Replace("&", "and");

            // Remove invalid chars
            text = Regex.Replace(text, @"[^a-z0-9\s-]", "");

            // Convert multiple spaces/hyphens into one space
            text = Regex.Replace(text, @"[\s-]+", " ").Trim();

            // Replace spaces with hyphens
            text = text.Replace(" ", "-");

            return text;
        }
    }
}
