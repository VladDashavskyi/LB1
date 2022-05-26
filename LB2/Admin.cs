using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Enum;
using LB2.Model;

namespace Lab2
{
    public class Admin : User
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

        public static int WorksMenu()
        {
            bool isValidate = true;
            bool isSoftDelete = false;
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"in\Input.json");
            var statusFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"in\Status.json");
            var inputFile = Advertisement.GetListDictionaryFromFile(file, false, isValidate);
            Handler<Menu> handler = new Handler<Menu>(file, statusFile, isValidate);
            Dictionary<string, object> arg = new Dictionary<string, object>();
            int rowId = 0;
            string key = string.Empty;
            string value = string.Empty;
            string errorMessage = string.Empty;

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
                                Console.WriteLine("Enter ID");
                                int.TryParse(Console.ReadLine(), out rowId);
                                break;
                            case (int)AdminMenu.Reject:
                                Console.WriteLine("Enter ID");
                                int.TryParse(Console.ReadLine(), out rowId);
                                Console.WriteLine("Enter error message");
                                errorMessage = Console.ReadLine();
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
                                case(int)AdminMenu.LogOut:
                                return (int)AdminMenu.LogOut;
                                break;
                        }

                        if (menuId == (int)AdminMenu.Search || (menuId == (int)AdminMenu.Approve && !isSoftDelete))
                        {
                            Menu menu = (Menu)System.Enum.Parse(typeof(Menu), ((AdminMenu)menuId).ToString());
                            handler.HandlerRun(menu, rowId, arg);
                        }

                        if (menuId == (int)AdminMenu.Approve || menuId == (int)AdminMenu.Reject)
                        {
                            WriteStatusFile(statusFile, rowId, errorMessage);
                        }

                        arg = new Dictionary<string, object>();
                        key = String.Empty;
                        value = String.Empty;
                        rowId = 0;
                        if (menuId != (int)AdminMenu.Search)
                            Advertisement.WriteConsoleDictionary(Advertisement.ReadStatusModel(statusFile, file, string.Empty, true));
                        PrintMenu();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        private static void WriteStatusFile(string statusFile, int id, string message)
        {
            var statusFileModel = Advertisement.ParceFileToModel<StatusModel>(statusFile);

            var result = statusFileModel.FirstOrDefault(w => w.ID == id);

            result.Message = message;
            result.Status = string.IsNullOrEmpty(message) ? Status.Approved.ToString() : Status.Rejected.ToString();

            Advertisement.WriteToJsonFile(statusFileModel, statusFile);
        }
    }
}