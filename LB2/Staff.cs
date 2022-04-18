using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LB2.Model;
using Newtonsoft.Json;
using Lab2.Enum;

namespace Lab2
{
    public class Staff: User
    {
        public int? Salary { get; set;}
        public DateTime? FirstDayInCompany { get; set; }

        private static void PrintMenu()
        {
            Console.WriteLine("MENU");
            foreach (StaffMenu menu in (StaffMenu[])System.Enum.GetValues(typeof(StaffMenu)))
            {
                Console.WriteLine(String.Join(" - ", (int)menu, menu));
            }
            
            Console.WriteLine("Make a choice" + "\r\n");
        }

        public static void WorksMenu()
        {
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
                            case (int)StaffMenu.Add:
                                Advertisement.WriteConsoleDictionary(ReadStatusModel());
                                break;
                                
                            case (int)StaffMenu.Remove:
                                break;
                            case (int)StaffMenu.Update:
                                break;
                            case (int)StaffMenu.Filter:
                                break;
                            case (int)StaffMenu.Sort:
                                break;
                        }
                    }

                    //Menu menu = (Menu)System.Enum.Parse(typeof(Menu), action);
                    //handler.HandlerRun(menu, rowId, arg);
                    //arg = new Dictionary<string, object>();
                    //key = String.Empty;
                    //value = String.Empty;
                    //rowId = 0;
                    //PrintMenu();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static List<Dictionary<string, object>> ReadStatusModel()
        {
            var statusFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"in\Status.json");
            var inputFile = Advertisement.GetListDictionaryFromFile(statusFile, false);

            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"in\Input.json");
            var inputModel = Advertisement.GetListDictionaryFromFile(file, false);

            List<int> filterModel = new List<int>();
            foreach (var item in inputFile)
            {
                foreach (KeyValuePair<string, object> kvp in item)
                {
                    if (kvp.Key == "Email" && kvp.Value.ToString().Contains("staf1@gmail.com"))
                    {
                        filterModel.Add(int.Parse(item.FirstOrDefault( x=> x.Key == "ID").Value.ToString()));
                    }
                }
            }

            List<Dictionary<string, object>> outputModel = new List<Dictionary<string, object>>();

            foreach (var item in filterModel)
            {
                foreach (var itemOut in inputModel)
                {
                    foreach (KeyValuePair<string, object> kvp in itemOut.Where(w => w.Key == "ID"))
                    {
                        if (kvp.Value.ToString() == item.ToString())
                        {
                            outputModel.Add(itemOut);
                            continue;
                        }
                    }
                }
            }

            return outputModel;
        }

    }
}
