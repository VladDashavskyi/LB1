using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Enum;

namespace Lab2
{
    public class Admin :User
    {
        private static void PrintMenu()
        {
            Console.WriteLine("MENU");
            foreach (AdminMenu menu in (AdminMenu[])System.Enum.GetValues(typeof(AdminMenu)))
            {
                Console.WriteLine(String.Join(" - ", (int)menu, menu));
            }

            Console.WriteLine("Make a choice" + "\r\n");
        }
        
        public static void WorksMenu()
        {
            bool isValidate = true;
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"in\Input.json");
            var statusFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"in\Status.json");
            var inputFile = Advertisement.GetListDictionaryFromFile(file, false, isValidate);
            Handler<Menu> handler = new Handler<Menu>(file, statusFile, isValidate);
            Dictionary<string, object> arg = new Dictionary<string, object>();
            int rowId = 0;
            string key = string.Empty;
            string value = string.Empty;

            Advertisement.WriteConsoleDictionary(Advertisement.ReadStatusModel(statusFile, file, String.Empty, true));

            PrintMenu();
            string action = string.Empty;
            try
            {
                while (action != "4")
                {
                    action = Console.ReadLine();
                    int menuId = 0;

                    if (int.TryParse(action, out menuId))
                    {
                        switch (menuId)
                        {
                            case (int)AdminMenu.Approve:
                                Update();
                                break;
                            case (int)AdminMenu.Reject:

                                break;
                            case (int)AdminMenu.Search:
                                var columns = Advertisement.PrintFileColumnModel(Advertisement.ReadStatusModel(statusFile, file, String.Empty, true), true);
                                Console.WriteLine("Choose column");
                                key = Console.ReadLine();
                                Console.WriteLine("Enter value");
                                value = Console.ReadLine();
                                arg.Add("Role", Enum.Role.Admin);
                                arg.Add(key, value);
                                break;
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Update()
        {

        }

    }
}
