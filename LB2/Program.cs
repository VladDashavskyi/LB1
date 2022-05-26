using LB2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Lab2.Enum;

namespace Lab2
{
    public class Program
    {
        static void Main(string[] args)
        {
            var userFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"in\User.json");
            Console.WriteLine("Register or Login");
            Console.WriteLine("0 - Login, 1 - Registration");
            int.TryParse(Console.ReadLine(), out int result);
            if (result == 0)
            {
                Console.WriteLine("Enter email");
                string em = Validation.ValidateEmail(Console.ReadLine());
                Console.WriteLine("Enter password");
                string pass = Console.ReadLine();
                var r = Register.CheckUser(userFile, em, pass);
                while (r == 7)                  
                {
                    Console.WriteLine("Enter email");
                    em = Validation.ValidateEmail(Console.ReadLine());
                    Console.WriteLine("Enter password");
                    pass = Console.ReadLine();
                    r = Register.CheckUser(userFile, em, pass);
                }
            }

            else
            {
                var inputFile = Advertisement.GetListDictionaryFromFile(userFile, false);
                Dictionary<string, object> arg = new Dictionary<string, object>();
                AddUser(inputFile, arg, userFile);
            }
        }

        private static void PrintMenu()
        {
            Console.WriteLine("MENU");
            foreach (Menu menu in (Menu[])System.Enum.GetValues(typeof(Menu)))
            {
                Console.WriteLine(String.Join(" - ", (int)menu, menu));
            }

            Console.WriteLine("Make a choice" + "\r\n");
        }

        private static void AddUser(List<Dictionary<string, object>> inputFile, Dictionary<string, object> arg, string file)
        {
            var columns = Advertisement.PrintFileColumnModel(inputFile, false);

            foreach (var column in columns)
            {
                Console.WriteLine("Add value for column " + column);

                if (column == "FirstName")
                {
                    arg.Add(column, Validation.ValidateName(Console.ReadLine()));
                    continue;
                }
                if (column == "LastName")
                {
                    arg.Add(column, Validation.ValidateName(Console.ReadLine()));
                    continue;
                }
                if (column == "Email")
                {
                    arg.Add(column, Validation.ValidateEmail(Console.ReadLine()));
                    continue;
                }
                if (column == "Role")
                {
                    string role = Console.ReadLine();
                    if (!System.Enum.IsDefined(typeof(Role), role))
                    {
                        throw new Exception("ValidationError: Role - is not valid");
                    }
                    else
                    {
                        arg.Add(column, role);
                        continue;
                    }
                }
                if (column == "Password")
                {
                    arg.Add(column, User.GetHashString(Validation.ValidatePassword(Console.ReadLine())));
                    continue;
                }
                if (column == "Salary")
                {
                    arg.Add(column, Validation.ValidateSalary(Console.ReadLine()));
                    continue;
                }
                if (column == "FirstDateInCompany")
                {
                    arg.Add(column, Validation.ValidateDate(Console.ReadLine()));
                    continue;
                }
                else
                {
                    arg.Add(column, Console.ReadLine());
                    continue;
                }
            }

            inputFile.Add(arg);
            Advertisement.AddRow(file, inputFile);
            inputFile = Advertisement.GetListDictionaryFromFile(file);

        }
    }
}