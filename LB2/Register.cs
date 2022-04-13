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
                    Admin admin = new Admin();
                    admin.Role = user.Role;
                }
                else
                if (user.Role == "Staff")
                {
                    Staff staff = new Staff();  
                    staff.Role = user.Role;
                    staff.Salary = user.Salary.HasValue ? user.Salary.Value : null;
                    staff.FirstDayInCompany = user.FirstDateInCompany.HasValue ? user.FirstDateInCompany.Value : null;

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

    }
}
