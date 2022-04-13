using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LB2.Model;
using Newtonsoft.Json;

namespace Lab2
{
    public static class Advertisement
    {
        public static List<T> ParceFileToModel<T>(string file)
        {
            try
            {
                List<T> outList = new List<T>();

                using (StreamReader r = new StreamReader(file))
                {
                    string json = r.ReadToEnd();
                    outList = JsonConvert.DeserializeObject<List<T>>(json);
                }

                Console.WriteLine($"Import file - count of rows: {outList.Count}");
                return outList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static void WriteToJsonFile<T>(List<T> inputModels, string file)
        {
            using (StreamWriter newFile = File.CreateText(file))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(newFile, inputModels);
            }
        }

        public static List<T> AddRow<T>(T newRow, string file)
        {
            List<T> input = ParceFileToModel<T>(file);
            input.Add(newRow);
            WriteToJsonFile<T>(input, file);
            return input;
        }
        public static List<T> DeleteRow<T>(T deleteRow, string file)
        {
            var input = ParceFileToModel<T>(file);
            input.Remove(deleteRow);
            WriteToJsonFile<T>(input, file);
            return input;
        }

        public static List<InputModel> UpdateRow(List<InputModel> input, string updateId, string property, string value)
        {
            int.TryParse(updateId, out int id);
            foreach (var row in input.Where(w => w.ID == id))
            {
                if (property == "URL")
                {
                    row.URL = Validation.ValidateURL(value);
                }
                if (property == "Price")
                {
                    row.Price = Validation.ValidatePrice(value);
                }
                if (property == "StartDate")
                {
                    row.StartDate = Validation.ValidateDate(value);
                }

            }
            return input;
        }

        public static List<T> Sort<T>(List<T> input, string property, string file, int order = 0)
        {
            List<T> sortInput;
            if (order != 0)
            {
                sortInput = input.OrderBy(p => p.GetType()
                    .GetProperty(property)
                    .GetValue(p, null)).ToList();
            }
            else
            {
                sortInput = input.OrderByDescending(p => p.GetType()
                    .GetProperty(property)
                    .GetValue(p, null)).ToList();
            }

            WriteToJsonFile<T>(sortInput, file);

            return sortInput;
        }

        public static List<T> Filter<T>(string file, Dictionary<string, string> filter)
        {
            

            return new List<T>();
        }

        public static void PrintFileModel(List<InputModel> inputModels)
        {
            if (inputModels == null)
            {
                throw new Exception("Invalid input file");
            }
            foreach (var row in inputModels)
            {
                Console.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                    row.ID,
                    row.URL,
                    row.StartDate,
                    row.EndDate,
                    row.Price,
                    row.Title,
                    row.PhotoURl,
                    row.TransactionNumber));
            }
        }
    }
}

