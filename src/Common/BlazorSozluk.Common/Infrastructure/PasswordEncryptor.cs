using System.Security.Cryptography;
using System.Text;

namespace BlazorSozluk.Common.Infrastructure;

public class PasswordEncryptor //encrpt işlemleri yapıldı
{
    //detaylı bilgi için => https://www.cozumpark.com/community/security-4/1025/ 
    //Message-Digest algorithm 5 hashing fonk, kısaca decrypti olmayan bir hash fonk aynı zmaanda alandan da tasarruf sağlar anladıgım kadarıyla
    public static string Encrypt (string pwd)
    {
        using var md5 = MD5.Create ();
        byte[] inputBytes = Encoding.ASCII.GetBytes (pwd);
        byte[] hashBytes = md5.ComputeHash (inputBytes);

        return Convert.ToHexString (hashBytes);
    }
}
