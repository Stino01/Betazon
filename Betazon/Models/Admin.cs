namespace Betazon.Models
{
    public class Admin
    {
        public int CustomerID { get; set; }
        public string? Title { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? Phone { get; set; }
        public string PasswordCrypt { get; set; }
        public string Key { get; set; }
        public string IV { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
