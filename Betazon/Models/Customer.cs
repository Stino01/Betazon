namespace Betazon.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public byte? NameStyle { get; set; }
        public string? Title { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string? Suffix { get; set; }
        public string? CompanyName { get; set; }
        public string? SalesPerson { get; set; }
        public string? EmailAddress { get; set; }
        public string? Phone { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Customer(int customerID, byte? nameStyle, string? title, string firstName, string? middleName, string lastName, string? suffix, string? companyName, string? salesPerson, string? emailAddress, string? phone, string passwordHash, Guid rowguid, DateTime modifiedDate, string passwordSalt = "")
        {
            CustomerID = customerID;
            NameStyle = nameStyle;
            Title = title;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Suffix = suffix;
            CompanyName = companyName;
            SalesPerson = salesPerson;
            EmailAddress = emailAddress;
            Phone = phone;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Rowguid = rowguid;
            ModifiedDate = modifiedDate;
        }
    }
       
}
