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
            int.TryParse(Console.ReadLine(),out int result);
            if (result == 0)
            {             
                Console.WriteLine("Enter email");
                string em = Validation.ValidateEmail(Console.ReadLine());
                Console.WriteLine("Enter password");
                string pass = Console.ReadLine();
                Register.CheckUser(userFile, em, pass);
            }
            else
            {
                var inputFile = Advertisement.GetListDictionaryFromFile(userFile, false);
                Dictionary<string, object> arg = new Dictionary<string, object>();
                AddUser(inputFile, arg, userFile);
            }
            Handler<Menu> handler = new Handler<Menu>(file, IsValidate);
            var inputFile = Advertisement.GetListDictionaryFromFile(file, true, IsValidate);

            Dictionary<string, object> arg = new Dictionary<string, object>();
            int rowId = 0;
            string key = string.Empty;
            string value = string.Empty;

            //bool IsValidate = true;
            //var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"in\Input.json");
            //var statusFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"in\Status.json");
            //var userFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"in\User.json");
            //Handler<Menu> handler = new Handler<Menu>(file);
            //var inputFile = Advertisement.GetListDictionaryFromFile(file);

            //Dictionary<string, object> arg = new Dictionary<string, object>();
            //int rowId = 0;
            //string key = string.Empty;
            //string value = string.Empty;

            string action = string.Empty;

                while (action != "6")
                {
                        action = Console.ReadLine();
                        int menuId = 0;

            //PrintMenu();

            //string action = string.Empty;
            //try
            //{
            //    while (action != "6")
            //    {
            //        action = Console.ReadLine();
            //        int menuId = 0;

            //        if (int.TryParse(action, out menuId))
            //        {
            //            switch (menuId)
            //            {
            //                case (int)Menu.Add:
            //                    Add(inputFile, arg, IsValidate);
            //                    break;
            //                case (int)Menu.Remove:
            //                    Console.WriteLine("ID");
            //                    int.TryParse(Console.ReadLine(), out rowId);
            //                    break;
            //                case (int)Menu.Search:
            //                    var columns = Advertisement.PrintFileColumnModel(inputFile, true);
            //                    Console.WriteLine("Choose column");
            //                    key = Console.ReadLine();
            //                    Console.WriteLine("Choose order");
            //                    value = Console.ReadLine();
            //                    arg.Add(key, value);
            //                    break;
            //                case (int)Menu.Sort:
            //                    columns = Advertisement.PrintFileColumnModel(inputFile, true);
            //                    Console.WriteLine("Choose column");
            //                    key = Console.ReadLine();
            //                    Console.WriteLine("Choose order");
            //                    value = Console.ReadLine();
            //                    arg.Add(key, value);
            //                    break;
            //                case (int)Menu.Update:

            //                    rowId = Update(inputFile, arg, IsValidate);

            //                    break;
            //            }
            //        }

            //        Menu menu = (Menu)System.Enum.Parse(typeof(Menu), action);
            //        handler.HandlerRun(menu, rowId, arg);
            //        arg = new Dictionary<string, object>();
            //        key = String.Empty;
            //        value = String.Empty;
            //        rowId = 0;
            //        PrintMenu();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
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

                        if (int.TryParse(action, out menuId))
                        {

                            switch (menuId)
                            {
                                case (int)Menu.Add:
                                    Add(inputFile, arg, IsValidate);
                                    break;
                                case (int)Menu.Remove:
                                    Console.WriteLine("ID");
                                    int.TryParse(Console.ReadLine(), out rowId);
                                    break;
                                case (int)Menu.Search:
                                    var columns = Advertisement.PrintFileColumnModel(inputFile, true);
                                    Console.WriteLine("Choose column");
                                    key = Console.ReadLine();
                                    Console.WriteLine("Choose order");
                                    value = Console.ReadLine();
                                    arg.Add(key, value);
                                    break;
                                case (int)Menu.Sort:
                                    columns = Advertisement.PrintFileColumnModel(inputFile, true);
                                    Console.WriteLine("Choose column");
                                    key = Console.ReadLine();
                                    Console.WriteLine("Choose order");
                                    value = Console.ReadLine();
                                    arg.Add(key, value);
                                    break;
                                case (int)Menu.Update:
        private static void AddUser(List<Dictionary<string, object>> inputFile, Dictionary<string, object> arg, string file)
        {
            var columns = Advertisement.PrintFileColumnModel(inputFile, false);

                                    rowId = Update(inputFile, arg, IsValidate);
            foreach (var column in columns)
            {
                Console.WriteLine("Add value for column " + column);

                                    break;
                            }
                        }


                        Menu menu = (Menu)System.Enum.Parse(typeof(Menu), action);
                        handler.HandlerRun(menu, rowId, arg);
                        arg = new Dictionary<string, object>();
                        key = String.Empty;
                        value = String.Empty;
                        rowId = 0;
                        PrintMenu();
                                       
                    }
            }
            catch (Exception ar)
            {
                Console.WriteLine(ar.Message);
            }
        }

        private static void PrintMenu()
        {
            Console.WriteLine("MENU");
            foreach (Menu menu in (Menu[])System.Enum.GetValues(typeof(Menu)))
            {
                Console.WriteLine(String.Join(" - ", (int)menu, menu));
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
                    arg.Add(column, Validation.ValidatePassword(Console.ReadLine()));
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
        private static void Add(List<Dictionary<string, object>> inputFile, Dictionary<string, object> arg, bool isValidate = true)
        {
            var columns = Advertisement.PrintFileColumnModel(inputFile, false);

            foreach (var column in columns)
            {
                Console.WriteLine("Add value for column " + column);

                if (isValidate)
                {
                    if (column == "ID")
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            var valid = Validation.ValidateId(Console.ReadLine());
                            if (valid.Item2 == true)
                            {
                                arg.Add(column, valid.Item1);
                                break;
                            }

                            if(i == 3)
                            {
                                throw new Exception("Validation - ");
                            }

                        }
                        continue;

                    }
                    if (column == "URL")
                    {

                        for (int i = 0; i < 4; i++)
                        {
                            var valid = Validation.ValidateURL(Console.ReadLine());
                            if (valid.Item2 == true)
                            {
                                arg.Add(column, valid.Item1);
                                break;
                            }

                            if (i == 3)
                            {
                                throw new Exception("Validation - ");
                            }

                        }
                        continue;
                    } if (column == "StartDate")
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            var valid = Validation.ValidateDate(Console.ReadLine());

                            if (valid.Item2 == true)
                            {
                                arg.Add(column, valid.Item1.Date.ToString("MM/dd/yyyy"));
                                break;
                            }

                            if (i == 3)
                            {
                                throw new Exception("Validation - ");
                            }

                        }
                        continue;                        
                    }if (column == "EndDate")
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            var valid = Validation.ValidateDate(Console.ReadLine());

                            if (valid.Item2 == true)
                            {
                                arg.Add(column, valid.Item1.Date.ToString("MM/dd/yyyy"));
                                break;
                            }

                            if (i == 3)
                            {
                                throw new Exception("Validation - ");
                            }

                        }
                        continue;
                    }
                    if (column == "Price")
                    {

                        for (int i = 0; i < 4; i++)
                        {
                            var valid = Validation.ValidatePrice(Console.ReadLine());

                        if (valid.Item2 == true)
                            {
                                arg.Add(column, Math.Round(valid.Item1, 2));
                                break;
                            }

                            if (i == 3)
                            {
                                throw new Exception("Validation - ");
                            }
                        }                        
                        continue;

                    }if (column == "Title")
                    {
                        arg.Add(column, Validation.ValidateTitle(Console.ReadLine()));
                        continue;
                    }if (column == "PhotoURL")
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            var valid = Validation.ValidateURL(Console.ReadLine());
                            if (valid.Item2 == true)
                            {
                                arg.Add(column, valid.Item1);
                                break;
                            }

                            if (i == 3)
                            {
                                throw new Exception("Validation - ");
                            }

                        }
                        continue;
                    }
                    if (column == "TransactionNumber")
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            var valid = Validation.ValidateTransactionNumber(Console.ReadLine());
                        if (valid.Item2 == true)
                            {
                                arg.Add(column, valid.Item1);
                                break;
                            }

                            if (i == 3)
                            {
                                throw new Exception("Validation - ");
                            }
                        }
                        continue;
                    }
                    else
                    {
                        arg.Add(column, Console.ReadLine());
                        continue;
                    }
                }
                else
                {
                    arg.Add(column, Console.ReadLine());
                    continue;
                }
            }

        }

        private static int Update(List<Dictionary<string, object>> inputFile, Dictionary<string, object> arg, bool isValidate = true)
        {
            var columns = Advertisement.PrintFileColumnModel(inputFile, true, false);
            Console.WriteLine("Choose ID");
            int.TryParse(Console.ReadLine(), out int rowId);

            Console.WriteLine("Choose column");
            string key = Console.ReadLine();
            if (isValidate)
            {
                if (key == "URL")
                {
                    Console.WriteLine("New value");
                    var value = Validation.ValidateURL(Console.ReadLine());
                    arg.Add(key, value.Item1);
                }if(key == "StartDate")
                {
                    Console.WriteLine("New value");
                    var value = Validation.ValidateDate(Console.ReadLine());
                    arg.Add(key, value.Item1.Date.ToString("MM/dd/yyyy"));
                }if(key == "EndDate")
                {
                    Console.WriteLine("New value");
                    var value = Validation.ValidateDate(Console.ReadLine());
                    arg.Add(key, value.Item1.Date.ToString("MM/dd/yyyy"));
                }if(key == "Price")
                {
                    Console.WriteLine("New value");
                    var value = Validation.ValidatePrice(Console.ReadLine());
                    arg.Add(key, Math.Round(value.Item1, 2));
                }if (key == "Title")
                {
                    Console.WriteLine("New value");
                    string value = Validation.ValidateTitle(Console.ReadLine());
                    arg.Add(key, value);
                }if (key == "PhotoURL")
                {
                    Console.WriteLine("New value");
                    var value = Validation.ValidateURL(Console.ReadLine());
                    arg.Add(key, value.Item1);
                }if (key == "TransactionNumber")
                {
                    Console.WriteLine("New value");
                    var value = Validation.ValidateTransactionNumber(Console.ReadLine());
                    arg.Add(key, value.Item1);
                }                
            }
            else
            {
                Console.WriteLine("New value");
                string value = Console.ReadLine();
                arg.Add(key, value);
            }
            return rowId;

        }
    }
}