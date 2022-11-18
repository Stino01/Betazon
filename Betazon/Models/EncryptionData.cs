namespace Betazon.Models
{
    public class EncryptionData
    {
        public int Id { get; set; }
        public string EncryptionAlgorithm { get; set; }
        public string EncryptedValue { get; set; }
        public string AesKey { get; set; }
        public string AesIV { get; set; }
        public bool IsMatch { get; set; }

        public EncryptionData(string encryptionAlgorithm = "", string aesKey = "", string aesIV = "")
        {
            EncryptionAlgorithm = encryptionAlgorithm;
            AesKey = aesKey;
            AesIV = aesIV;
        }
    }
}
