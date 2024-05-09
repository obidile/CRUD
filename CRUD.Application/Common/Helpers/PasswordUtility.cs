using System.Security.Cryptography;
using System.Text;

namespace Craft.Application.Common.Helpers;

public class PasswordUtility
{
    public static void CreateHash(string keyword, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using HMACSHA512 hMACSHA = new HMACSHA512();
        passwordSalt = hMACSHA.Key;
        passwordHash = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(keyword));
    }

    public static bool VerifyHash(string keyword, byte[] storedHash, byte[] storedSalt)
    {
        using HMACSHA512 hMACSHA = new HMACSHA512(storedSalt);
        return hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(keyword)).SequenceEqual(storedHash);
    }
}
