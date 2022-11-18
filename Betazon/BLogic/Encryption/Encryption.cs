using Betazon.Models;
using System.Security.Cryptography;
using System.Text;

namespace Betazon.BLogic.Encryption
{
    public class Encryption
    {
        public EncryptionData EncryptString(string StrValue, string AlgorithmType)
        {
            if (string.IsNullOrEmpty(StrValue)) return null;

            EncryptionData encryptionData = new EncryptionData();
            HashAlgorithm hashAlgorithm;

            if (AlgorithmType.ToUpper() != "AES")
            {
                switch (AlgorithmType.ToUpper())
                {
                    case "SHA1":
                        hashAlgorithm = SHA1.Create();
                        break;
                    case "SHA256":
                        hashAlgorithm = SHA256.Create();
                        break;
                    case "SHA512":
                        hashAlgorithm = SHA512.Create();
                        break;
                    default:
                        hashAlgorithm = SHA256.Create();
                        break;
                }

                byte[] bytes = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(StrValue));
                string salt = string.Empty;

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("X2"));
                }
                encryptionData = new EncryptionData
                {
                    EncryptionAlgorithm = AlgorithmType,
                    EncryptedValue = builder.ToString()
                };
            }
            else if (AlgorithmType.ToUpper() == "AES")
            {
                using (Aes myAes = Aes.Create())
                {
                    byte[] encrypted = EncryptStringToBytes_Aes(StrValue, myAes.Key, myAes.IV);
                    string roundtrip = DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);

                    encryptionData = new EncryptionData
                    {
                        EncryptionAlgorithm = AlgorithmType,
                        EncryptedValue = Convert.ToBase64String(encrypted),
                        AesKey = Convert.ToBase64String(myAes.Key),
                        AesIV = Convert.ToBase64String(myAes.IV),
                        IsMatch = StrValue == roundtrip
                    };
                }
            }
            return encryptionData;
        }

        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        public string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
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
