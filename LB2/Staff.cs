﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LB2.Model;
using Newtonsoft.Json;
using Lab2.Enum;

namespace Lab2
{
    public class Staff : User
    {
        public int? Salary { get; set; }
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

        public static void WorksMenu(string email)
        {
            PrintMenu();
            Advertisement.WriteConsoleDictionary(ReadStatusModel(email));
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

        private static List<Dictionary<string, object>> ReadStatusModel(string email)
        {

            var statusFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"in\Status.json");
            var statusModel = Advertisement.ParceFileToModel<StatusModel>(statusFile).Where(w => w.Email == email);

            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"in\Input.json");
            var inputModel = Advertisement.GetListDictionaryFromFile(file, false);

            List<Dictionary<string, object>> outputModel = new List<Dictionary<string, object>>();

            foreach (var item in statusModel)
            {
                foreach (var itemOut in inputModel)
                {
                    foreach (KeyValuePair<string, object> kvp in itemOut.Where(w => w.Key == "ID").ToList())
                    {
                        if (kvp.Value.ToString() == item.ID.ToString())
                        {
                            itemOut.Add("Status", item.Status);
                            itemOut.Add("Message", item.Message);
                            outputModel.Add(itemOut);

                        }
                    }
                }
            }

            return outputModel;
        }
    }
}