using System.Security.Cryptography;
using System.Text;

namespace MartEdu.Service.Extensions
{
    public static class StringExtensions
    {
        public static string Encrypt(this string password)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var uTF8Encoding = new UTF8Encoding();

                var data = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

                return Encoding.UTF8.GetString(data);
            }
        }
    }
}
