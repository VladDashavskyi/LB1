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
            PrintMenu();
            string action = string.Empty;
            try
            {
                while (action != "2")
                {
                    action = Console.ReadLine();
                    int menuId = 0;

                    if (int.TryParse(action, out menuId))
                    {
                        switch (menuId)
                        {
                            case (int)AdminMenu.Update:
                                Update();
                                break;
                            //case (int)AdminMenu.AddUser:
                            //    Register.AddUser();
                            //    break;
                            //case (int)AdminMenu.UpdateUser:
                            //    Register.UpdateUser();
                            //    break;
                            //case (int)AdminMenu.DeleteUser:
                            //    Register.DeleteUser();
                            //    break;
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

        public static void Update()
        {

        }

    }
}
