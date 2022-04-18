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
            bool IsValidate = true;

            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"in\Input.json");
            var statusFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"in\Status.json");
            var userFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"in\User.json");
            Handler<Menu> handler = new Handler<Menu>(file);
            var inputFile = Advertisement.GetListDictionaryFromFile(file);

            Dictionary<string, object> arg = new Dictionary<string, object>();
            int rowId = 0;
            string key = string.Empty;
            string value = string.Empty;


            PrintMenu();

            string action = string.Empty;
            try
            {
                while (action != "6")
                {
                    action = Console.ReadLine();
                    int menuId = 0;

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

                                rowId = Update(inputFile, arg, IsValidate);

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

        private static void Add(List<Dictionary<string, object>> inputFile, Dictionary<string, object> arg, bool isValidate = true)
        {
            var columns = Advertisement.PrintFileColumnModel(inputFile, false);

            foreach (var column in columns)
            {
                Console.WriteLine("Add value for column " + column);

                if (isValidate)
                {
                    if (column == "URL")
                    {
                        arg.Add(column, Validation.ValidateURL(Console.ReadLine()));
                        continue;
                    } if (column == "StartDate")
                    {
                        arg.Add(column, Validation.ValidateDate(Console.ReadLine()).Date.ToString("MM/dd/yyyy"));
                        continue;
                    }if (column == "EndDate")
                    {
                        arg.Add(column, Validation.ValidateDate(Console.ReadLine()).Date.ToString("MM/dd/yyyy"));
                        continue;
                    }if (column == "Price")
                    {
                        arg.Add(column, Validation.ValidatePrice(Console.ReadLine()));
                        continue;
                    }if (column == "Title")
                    {
                        arg.Add(column, Validation.ValidateTitle(Console.ReadLine()));
                        continue;
                    }if (column == "PhotoURL")
                    {
                        arg.Add(column, Validation.ValidateURL(Console.ReadLine()));
                        continue;
                    }if (column == "TransactionNumber")
                    {
                        arg.Add(column, Validation.ValidateTransactionNumber(Console.ReadLine()));
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
                    string value = Validation.ValidateURL(Console.ReadLine());
                    arg.Add(key, value);
                }if(key == "StartDate")
                {
                    Console.WriteLine("New value");
                    var value = Validation.ValidateDate(Console.ReadLine()).Date.ToString("MM/dd/yyyy");
                    arg.Add(key, value);
                }if(key == "EndDate")
                {
                    Console.WriteLine("New value");
                    var value = Validation.ValidateDate(Console.ReadLine()).Date.ToString("MM/dd/yyyy");
                    arg.Add(key, value);
                }if(key == "Price")
                {
                    Console.WriteLine("New value");
                    var value = Validation.ValidatePrice(Console.ReadLine());
                    arg.Add(key, value);
                }if (key == "Title")
                {
                    Console.WriteLine("New value");
                    string value = Validation.ValidateTitle(Console.ReadLine());
                    arg.Add(key, value);
                }if (key == "PhotoURL")
                {
                    Console.WriteLine("New value");
                    string value = Validation.ValidateURL(Console.ReadLine());
                    arg.Add(key, value);
                }if (key == "TransactionNumber")
                {
                    Console.WriteLine("New value");
                    string value = Validation.ValidateTransactionNumber(Console.ReadLine());
                    arg.Add(key, value);
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