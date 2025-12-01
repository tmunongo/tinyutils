using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace tinyutils.Services;

public class TextUtilsService
{
    public string ToBase64(string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        return Convert.ToBase64String(bytes);
    }

    public string FromBase64(string input)
    {
        try
        {
            var bytes = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(bytes);
        }
        catch
        {
            throw new ArgumentException("Invalid Base64 string");
        }
    }

    public string Slugify(string input)
    {
        var slug = input.ToLowerInvariant();

        slug = RemoveAccents(slug);

        slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
        slug = Regex.Replace(slug, @"\s+", "-");
        slug = Regex.Replace(slug, @"-+", "-");

        return slug.Trim('-');
    }

    public string GenerateUuid()
    {
        return Guid.NewGuid().ToString();
    }

    public string ComputeHash(string input, string algorithm)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        byte[] hash;

        switch (algorithm.ToUpperInvariant())
        {
            case "MD5":
                hash = MD5.HashData(bytes);
                break;
            case "SHA1":
                hash = SHA1.HashData(bytes);
                break;
            case "SHA256":
                hash = SHA256.HashData(bytes);
                break;
            case "SHA512":
                hash = SHA256.HashData(bytes);
                break;
            default:
                throw new ArgumentException($"Unsupported algorithm: {algorithm}");
        }

        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }

    private string RemoveAccents(string text)
    {
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = char.GetUnicodeCategory(c);
            if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
}