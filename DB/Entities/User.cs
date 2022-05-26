namespace DB.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public decimal? Salary { get; set; }
        public DateTime? FirstDateInCompany { get; set; }
        public int RoleId { get; set; }
        public Role Roles { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
        //public ICollection<DocumentExt> DocumentExts { get; set; }
    }
}
