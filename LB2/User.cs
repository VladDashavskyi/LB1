using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public abstract class User
    {
        private string firstName;
        private string lastName;
        private string email;
        private string role;
        private string password;

        public static string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }

        public void setHashedPassword(string hashedPsw)
        {
            this.password = hashedPsw;
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                    firstName = Validation.ValidateName(value);  
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                   lastName = Validation.ValidateName(value);
            }
        }

        public string Email
        {
            get { return email; }
            set
            {

                email = Validation.ValidateEmail(value);
            }
        }

        public string Role
        {
            get { return role; }
            set
            {
                role = value;
            }
        }

        public string Password
        {
            get { return password; }
            set
            {             
                    password =Validation.ValidatePassword(GetStringSha256Hash(value));              
            }
        }
    }
}
