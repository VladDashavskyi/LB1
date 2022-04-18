using LB2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Register
    {
        public static void CheckUser(string file, string login, string password)
        {
            var userCheck = Advertisement.ParceFileToModel<UserModel>(file);

            var user = userCheck.FirstOrDefault(x => x.Email == login && x.Password == password);

            if (user != null)
            {
                if (user.Role == "Admin")
                {
                    Console.WriteLine("Welcome {0},{1}, U logged as {2}", user.FirstName, user.LastName, user.Role);
                    Admin admin = new Admin();
                    admin.Role = user.Role;
                    Admin.WorksMenu();
                }
                else
                if (user.Role == "Staff")
                {
                    Console.WriteLine("Welcome {0},{1}, U logged as {2}", user.FirstName, user.LastName,user.Role);
                    Staff staff = new Staff();  
                    staff.Role = user.Role;
                    staff.Salary = user.Salary.HasValue ? user.Salary.Value : null;
                    staff.FirstDayInCompany = user.FirstDateInCompany.HasValue ? user.FirstDateInCompany.Value : null;
                    staff.Email = user.Email;
                    Staff.WorksMenu();
                }
                else
                {
                    Console.WriteLine("User ");    
                }
            }
            else
            {
                Console.WriteLine("dfgdfgdfgdfg");
            }
        }

        public static void AddUser()
        {
            
        }
        public static void UpdateUser()
        {

        }

        public static void DeleteUser()
        {

        }
    }
}
