using System.Text.RegularExpressions;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Services.Implementations;

public class SlugService : ISlugService
{
    public string Generate(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        text = text.Trim().ToLower();

        // حذف کاراکترهای خیلی مزاحم
        text = Regex.Replace(text, @"[^\w\s\u0600-\u06FF-]", "");

        // فاصله → خط تیره
        text = Regex.Replace(text, @"\s+", "-");

        // چند خط تیره پشت سر هم
        text = Regex.Replace(text, @"-+", "-");

        return text.Trim('-');
    }
}