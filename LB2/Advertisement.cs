using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
        public static List<T> DeleteRow<T>(List<T> inputModels, string property, object id, string file)
        {
            var newInputModels = new List<T>();
            foreach (var item in inputModels)
            {
                var propertyInfo = item.GetType().GetProperty(property);
                var propertyValue = propertyInfo.GetValue(item, null);
                if (propertyValue.ToString() == id.ToString())
                {
                    newInputModels.Remove(item);
                }
                else
                {
                    newInputModels.Add(item);
                }
            }
            WriteToJsonFile<T>(newInputModels, file);
            return newInputModels;
        }

        public static List<T> UpdateRow<T>(List<T> inputModels, T updateRow, string property, object id, string file)
        {
            var newInputModels = new List<T>();
            foreach (var item in inputModels)
            {
                var propertyInfo = item.GetType().GetProperty(property);
                var propertyValue = propertyInfo.GetValue(item, null);
                if (propertyValue == id)
                {
                    newInputModels.Remove(item);
                    newInputModels.Add(updateRow);
                }
                else
                {
                    newInputModels.Add(item);
                }
            }
            WriteToJsonFile<T>(newInputModels, file);
            return newInputModels;
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

        public static List<T> Filter<T>(List<T> inputModels, string property, string value)
        {
            var filterInputModels = new List<T>();
            foreach (var item in inputModels)
            {
                var propertyInfo = item.GetType().GetProperty(property);
                if (propertyInfo == null)
                {
                    throw new Exception("property does not exists");
                }

                var propertyValue = propertyInfo.GetValue(item, null).ToString();
                if (propertyValue == value)
                {
                    filterInputModels.Add(item);
                }
            }

            return filterInputModels;
        }

        public static void PrintFileModel<T>(List<T> inputModels)
        {
            if (inputModels == null)
            {
                throw new Exception("Invalid input file");
            }

            var props = typeof(T).GetProperties();

            foreach (var prop in props)
            {
                Console.Write("{0}\t", prop.Name);
            }
            Console.WriteLine();

            foreach (var item in inputModels)
            {
                foreach (var prop in props)
                {
                    Console.Write("{0}\t", prop.GetValue(item, null));
                }
                Console.WriteLine();
            }
        }
    }
}

