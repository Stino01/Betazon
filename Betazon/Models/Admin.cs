namespace Betazon.Models
{
    public class Admin
    {
        public int AdminID { get; set; }
        public string? Title { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? Phone { get; set; }
        public EncryptionData EncryptionData { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
