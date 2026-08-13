using System.Security.Cryptography;
using System.Text;

namespace Shortener.Services;

public class CodeGenerator
{
    public static string Generate(string longUrl)
    {
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(longUrl));
        return Convert.ToBase64String(hashBytes)
            .Replace("+", "-")
            .Replace("/", "_")
            .Replace("=", "")
            [..8];
    }
}
