using LB2.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2
{
    public class Program
    {
        static void Main(string[] args)
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"in\Input.json");
            var statusFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Status.json");
            var userFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"User.json") ?? String.Empty;


            try
            {
                //Console.WriteLine("Enter login");
                //var login = Console.ReadLine() ?? String.Empty;
                //Console.WriteLine("Enter password");
                //var password = Console.ReadLine() ?? String.Empty;

                //Register.CheckUser(userFile, login, password);
                //return;

                Console.WriteLine("MENU" + "\r\n"
                                 + "Search press 1" + "\r\n"
                                 + "Add press 2" + "\r\n"
                                 + "Delete press 3" + "\r\n"
                                 + "Edit press 4" + "\r\n"
                                 + "Sort press 5" + "\r\n"
                                 + "Exit press 6");

                Console.WriteLine();
                Console.WriteLine(@"in\Input.json");

                var list = Advertisement.ParceFileToModel<InputModel>(file);

                Advertisement.PrintFileModel(list);

                Console.WriteLine("Make a choice" + "\r\n");

                string key = string.Empty;

                while (key != "6")
                {
                    key = Console.ReadLine();
                    int menuId = 0;

                    List<InputModel> result;

                    if (int.TryParse(key, out menuId))
                    {
                        switch (menuId)
                        {
                            case (int)MenuEnum.search:
                                Console.WriteLine("Search");
                                Console.WriteLine("Enter property: ");
                                var pr = Console.ReadLine();
                                Console.WriteLine("Enter value: ");
                                var v = Console.ReadLine();
                                result = Advertisement.Filter(list, pr, v);
                                Advertisement.PrintFileModel(result);
                                break;
                            case (int)MenuEnum.add:
                                Console.WriteLine("Add");
                                Console.WriteLine("Enter ID:");
                                var inputId = Console.ReadLine();
                                Console.WriteLine("Enter URL:");
                                var inputUrl = Console.ReadLine();
                                Console.WriteLine("Enter Start date:");
                                var inputStartDate = Console.ReadLine();
                                Console.WriteLine("Enter End date:");
                                var inputEndDate = Console.ReadLine();
                                Console.WriteLine("Enter Price:");
                                var inputPrice = Console.ReadLine();
                                Console.WriteLine("Enter Title:");
                                var inputTitle = Console.ReadLine();
                                Console.WriteLine("Enter PhotoURL:");
                                var inputPhotoUrl = Console.ReadLine();
                                Console.WriteLine("Enter Transaction number:");
                                var inputTransactionNumber = Console.ReadLine();

                                var newRow = new InputModel
                                {
                                    ID = Validation.ValidateId(inputId),
                                    URL = Validation.ValidateURL(inputUrl),
                                    StartDate = Validation.ValidateDate(inputStartDate),
                                    EndDate = Validation.ValidateDate(inputEndDate),
                                    Price = Validation.ValidatePrice(inputPrice),
                                    Title = Validation.ValidateTitle(inputTitle),
                                    PhotoURl = Validation.ValidateURL(inputPhotoUrl),
                                    TransactionNumber = Validation.ValidateTransactionNumber(inputTransactionNumber)
                                };

                                result = Advertisement.AddRow(newRow, file);
                                Advertisement.PrintFileModel(result);
                                break;
                            case (int)MenuEnum.delete:
                                Console.WriteLine("Delete");
                                Console.WriteLine("Enter ID");
                                int p = 0;
                                int.TryParse(Console.ReadLine(), out p);
                                if (p == 0)
                                {
                                    Console.WriteLine("Error, ID isn't valid!");
                                    break;
                                }
                                int count = list.Count;

                                var deleteRow = list.FirstOrDefault(i => i.ID == p);
                                if (deleteRow != null)
                                {
                                    result = Advertisement.DeleteRow(list, "ID", p, file);

                                    if (count > result.Count)
                                    {
                                        Console.WriteLine("New list look:");
                                        Advertisement.PrintFileModel(result);
                                        break;
                                    }
                                    else
                                    {
                                        Advertisement.PrintFileModel(result);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Unknown ID");

                                }
                                break;
                            case (int)MenuEnum.edit:
                                Console.WriteLine("Edit");
                                Console.WriteLine("Enter row which u wanna to update");
                                string updateId = Console.ReadLine();
                                Console.WriteLine(
                                    "Choose property to edit: " + "\r\n"
                                 + "URL" + "\r\n"
                                 + "Price" + "\r\n"
                                 + "StartDate" + "\r\n");
                                string updatePropertyName = Console.ReadLine();
                                Console.WriteLine("Enter new value");
                                string newValue = Console.ReadLine();

                                int.TryParse(updateId, out int id);
                                var updateRow = list.FirstOrDefault(x => x.ID == id);
                                if (updateRow != null)
                                {
                                    if (updatePropertyName == "URL")
                                    {
                                        updateRow.URL = Validation.ValidateURL(newValue);
                                    }
                                    if (updatePropertyName == "Price")
                                    {
                                        updateRow.Price = Validation.ValidatePrice(newValue);
                                    }
                                    if (updatePropertyName == "StartDate")
                                    {
                                        updateRow.StartDate = Validation.ValidateDate(newValue);
                                    }
                                }

                                Advertisement.UpdateRow(list, updateRow, "ID", id, file);
                                Advertisement.PrintFileModel(list);
                                break;
                            case (int)MenuEnum.sort:
                                Console.WriteLine(
                                    "Choose property to sort: " + "\r\n"
                                 + "ID" + "\r\n"
                                 + "Title" + "\r\n"
                                 + "Price" + "\r\n");
                                string sorting = Console.ReadLine();
                                Console.WriteLine(
                                    "Choose order: 0 - desc, 1 - asc");
                                string stringOrder = Console.ReadLine();
                                int.TryParse(stringOrder, out int order);
                                Console.WriteLine("Sort");
                                list = Advertisement.Sort<InputModel>(list, sorting, file, order);
                                Advertisement.PrintFileModel(list);
                                break;
                            case (int)MenuEnum.exit:
                                Console.WriteLine("Exit");
                                break;
                            default:
                                Console.WriteLine("unknown comand");
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}