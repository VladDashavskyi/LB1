using System;

namespace LB4.DTO
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public decimal? Salary { get; set; }
        public DateTime? FirstDateInCompany { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
    }
}
