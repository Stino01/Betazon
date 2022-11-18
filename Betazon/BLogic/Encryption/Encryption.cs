using System.Security.Cryptography;
using System.Text;

namespace Betazon.BLogic.Encryption
{
    public class Encryption
    {
        public string EncryptString(string s)
        {
            string encryptResult = string.Empty;
            SHA512 sHA512 = SHA512.Create();
            byte[] bytes = sHA512.ComputeHash(Encoding.UTF8.GetBytes(s));

            foreach (byte theByte in bytes)
            {
                encryptResult += theByte.ToString("x2");
            }

            return encryptResult;
        }

        public string GetSalt()
        {
            var random = new RNGCryptoServiceProvider();

            // Maximum length of salt
            int max_length = 32;

            // Empty salt array
            byte[] salt = new byte[max_length];

            // Build the random bytes
            random.GetNonZeroBytes(salt);

            // Return the string encoded salt
            return Convert.ToBase64String(salt);
        }

        
    }
}
