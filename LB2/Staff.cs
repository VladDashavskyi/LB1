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
            bool isValidate = true;
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"in\Input.json");
            var statusFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"in\Status.json");
            var inputFile = Advertisement.GetListDictionaryFromFile(file, false, isValidate);
            Handler<Menu> handler = new Handler<Menu>(file, isValidate);
            Dictionary<string, object> arg = new Dictionary<string, object>();
            int rowId = 0;
            PrintMenu();
            Advertisement.WriteConsoleDictionary(ReadStatusModel(statusFile, file, email));
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
                                Add(inputFile, arg, isValidate);
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

                    Menu menu = (Menu)System.Enum.Parse(typeof(Menu), action);
                    handler.HandlerRun(menu, rowId, arg);
                    WriteStatusFile(statusFile, 18, email, action, message);
                    arg = new Dictionary<string, object>();
                    rowId = 0;
                    Advertisement.WriteConsoleDictionary(ReadStatusModel(statusFile, file, email));
                    PrintMenu();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static List<Dictionary<string, object>> ReadStatusModel(string statusFile, string file, string email)
        {

            
            var statusModel = Advertisement.ParceFileToModel<StatusModel>(statusFile).Where(w => w.Email == email);
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
        private static void Add(List<Dictionary<string, object>> inputFile, Dictionary<string, object> arg, bool isValidate = true)
        {
            var columns = Advertisement.PrintFileColumnModel(inputFile, false, false);
            arg.Add("ID", inputFile.Max(w => w.TryGetValue("ID", out object id)));
            foreach (var column in columns)
            {
                Console.WriteLine("Add value for column " + column);

                if (isValidate)
                {                   
                    //if (column == "ID")
                    //{
                    //    for (int i = 0; i < 4; i++)
                    //    {
                    //        var valid = Validation.ValidateId(Console.ReadLine());
                    //        if (valid.Item2 == true)
                    //        {
                    //            arg.Add(column, valid.Item1);
                    //            break;
                    //        }

                    //        if (i == 3)
                    //        {
                    //            throw new Exception("Validation - ");
                    //        }

                    //    }
                    //    continue;

                    //}
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
                    }
                    if (column == "StartDate")
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
                    if (column == "EndDate")
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

                    }
                    if (column == "Title")
                    {
                        arg.Add(column, Validation.ValidateTitle(Console.ReadLine()));
                        continue;
                    }
                    if (column == "PhotoURL")
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
                }
                if (key == "StartDate")
                {
                    Console.WriteLine("New value");
                    var value = Validation.ValidateDate(Console.ReadLine());
                    arg.Add(key, value.Item1.Date.ToString("MM/dd/yyyy"));
                }
                if (key == "EndDate")
                {
                    Console.WriteLine("New value");
                    var value = Validation.ValidateDate(Console.ReadLine());
                    arg.Add(key, value.Item1.Date.ToString("MM/dd/yyyy"));
                }
                if (key == "Price")
                {
                    Console.WriteLine("New value");
                    var value = Validation.ValidatePrice(Console.ReadLine());
                    arg.Add(key, Math.Round(value.Item1, 2));
                }
                if (key == "Title")
                {
                    Console.WriteLine("New value");
                    string value = Validation.ValidateTitle(Console.ReadLine());
                    arg.Add(key, value);
                }
                if (key == "PhotoURL")
                {
                    Console.WriteLine("New value");
                    var value = Validation.ValidateURL(Console.ReadLine());
                    arg.Add(key, value.Item1);
                }
                if (key == "TransactionNumber")
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
        private static void WriteStatusFile(string statusFile, int id, string email, string action, string message)
        {
            var statusFileModel = Advertisement.ParceFileToModel<StatusModel>(statusFile);

            StatusModel statusModel = new StatusModel 
            { 
                ID = id,
                Email = email, 
                Action = action, 
                Message = message, 
                Status = Status.Draft.ToString() 
            };

            statusFileModel.Add(statusModel);

            Advertisement.WriteToJsonFile(statusFileModel, statusFile);
        }
    }
}