using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LB2.Model;
using Newtonsoft.Json;

namespace Lab2
{
    internal class Staff: User
    {
        public int? Salary { get; set;}
        public DateTime? FirstDayInCompany { get; set; }      
        public static List<StatusModel> ParceFileToModel(string file)
        {
            try
            {
                List< StatusModel> outList = new List<StatusModel>();

                using (StreamReader r = new StreamReader(file))
                {
                    string json = r.ReadToEnd();
                    outList = JsonConvert.DeserializeObject<List<StatusModel>> (json);
                }

                Console.WriteLine($"Import file - count of rows: {outList.Count}");
                return outList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            static void WriteToJsonFile(List<StatusModel> inputModels, string file)
            {
                using (StreamWriter newFile = File.CreateText(file))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(newFile, inputModels);
                }
            }


        }


    }
}
