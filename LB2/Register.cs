using Lab2.Enum;
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
        public static int CheckUser(string file, string login, string password)
        {
            var userCheck = Advertisement.ParceFileToModel<UserModel>(file);

            var user = userCheck.FirstOrDefault(x => x.Email == login && x.Password == User.GetHashString(password));

            if (user != null)
            {
                if (user.Role == Role.Admin.ToString())
                {
                    Console.WriteLine("\r\n" + "Welcome {0}, {1}, U logged as {2}" + "\r\n", user.FirstName, user.LastName, user.Role);
                    Admin admin = new Admin();
                    admin.Role = user.Role;
                    return Admin.WorksMenu();
                }
                else
                if (user.Role == Role.Staff.ToString())
                {
                    Console.WriteLine("\r\n" + "Welcome {0}, {1}, U logged as {2}" + "\r\n", user.FirstName, user.LastName, user.Role);
                    Staff staff = new Staff();
                    staff.Role = user.Role;
                    staff.Salary = user.Salary.HasValue ? user.Salary.Value : null;
                    staff.FirstDayInCompany = user.FirstDateInCompany.HasValue ? user.FirstDateInCompany.Value : null;
                    staff.Email = user.Email;
                    return Staff.WorksMenu(staff.Email);
                }
                else
                {
                    Console.WriteLine("Role not found!");
                }
            }
            else
            {
                Console.WriteLine("User not found");
            }
            return 0;
        }


    }
}