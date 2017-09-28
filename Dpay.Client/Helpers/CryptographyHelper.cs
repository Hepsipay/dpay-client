using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Dpay.Client.Helpers
{
    public class CryptographyHelper
    {
        public static string HashSha256(string data)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            var hashstring = new SHA256Managed();

            var hash = hashstring.ComputeHash(bytes);

            return hash.Aggregate(string.Empty, (current, x) => current + string.Format("{0:x2}", x));
        }

        public static string HMacSha512(string data, string secretKey)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            var hashstring = new HMACSHA512(Encoding.UTF8.GetBytes(secretKey));
            var hash = hashstring.ComputeHash(bytes);
            return hash.Aggregate(string.Empty, (current, x) => current + string.Format("{0:x2}", x));
        }
    }
}
