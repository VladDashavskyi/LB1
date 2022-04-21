using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using LB2.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Lab2
{
    public class Advertisement
    {
        private readonly bool isValidate;
        public Advertisement(bool _isValidate)
        {
            _isValidate = isValidate;
        }
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

        public static IQueryable<JToken> ParceFileToModel(string file)
        {
            IQueryable<JToken> data ;
            using (StreamReader r = new StreamReader(file))
            {
                string stringJson = r.ReadToEnd();
                var json = JsonConvert.DeserializeObject<dynamic>(stringJson);
                data = ((JToken)json).Children().AsQueryable();
            }
            
            return data;
        }

        public static void WriteToJsonFile<T>(List<T> inputModel, string file)
        {
            try
            {
                using (StreamWriter newFile = File.CreateText(file))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(newFile, inputModel);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void AddRow(string file, List<Dictionary<string, object>> inputModel)
        {
            WriteToJsonFile(inputModel, file);
        }
        public static void DeleteRow<T>(string file, List<Dictionary<string, object>> inputModel, string keyRows,
            T id)
        {
            {
                try
                {
                    foreach (var item in inputModel)
                    {
                        foreach (KeyValuePair<string, object> kvp in item)
                        {
                            if (kvp.Key == keyRows && kvp.Value.ToString() == id.ToString())
                            {
                                inputModel.Remove(item);
                                WriteToJsonFile(inputModel, file);
                                return;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static void UpdateRow<T>(string file, List<Dictionary<string, object>> inputModel, string keyRows,
            T rowId, Dictionary<string, T> kv)
        {
            try
            {
                foreach (var item in inputModel)
                {
                    foreach (KeyValuePair<string, object> kvp in item)
                    {
                        if (kvp.Key == keyRows && kvp.Value.ToString() == rowId.ToString())
                        {
                            foreach (var k in kv)
                            {
                                item[k.Key] = k.Value;
                            }
                        }
                    }
                }

                WriteToJsonFile(inputModel, file);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void Sort(string file, List<Dictionary<string, object>> inputModel, string key, int order = 0)
        {
            try
            {
                List<Dictionary<string, object>> sortModel = new List<Dictionary<string, object>>();
                if (order == 0)
                    sortModel = inputModel.OrderBy(x => x.ContainsKey(key) ? x[key] : string.Empty).ToList();

                if (order == 1)
                    sortModel = inputModel.OrderByDescending(x => x.ContainsKey(key) ? x[key] : string.Empty).ToList();

                if (sortModel.Count > 0)
                    WriteToJsonFile(sortModel, file);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void Filter<T>(List<Dictionary<string, object>> inputModel, string key, T value)
        {
            try
            {
                List<Dictionary<string, object>> filterModel = new List<Dictionary<string, object>>();
                foreach (var item in inputModel)
                {
                    foreach (KeyValuePair<string, object> kvp in item)
                    {
                        if (kvp.Key == key && kvp.Value.ToString().Contains(value.ToString()))
                        {
                            filterModel.Add(item);
                        }
                    }
                }

                WriteConsoleDictionary(filterModel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static List<string> PrintFileColumnModel(List<Dictionary<string, object>> inputModel, bool printConsole = true, bool IsPrintId = true)
        {
            List<string> columnNames = new List<string>();

            foreach (var item in inputModel.Take(1))
            {
                if (printConsole)
                    Console.WriteLine("Columns name");
                foreach (KeyValuePair<string, object> kvp in item)
                {
                    if ((kvp.Key == "ID" && !IsPrintId))
                        continue;

                    columnNames.Add(kvp.Key);
                    if (printConsole)
                        Console.WriteLine(kvp.Key);

                }
            }

            return columnNames;
        }
         
        public static List<Dictionary<string, object>> GetListDictionaryFromFile(string file, bool printFileToConsole = true)
        {
            try
            {
                var parceFileToModel = ParceFileToModel(file);

                JArray dict = new JArray();

                List<Dictionary<string, object>> model = new List<Dictionary<string, object>>();
                foreach (var item in parceFileToModel)
                {

                    var d = ConvertToDictionary(item.ToString());
                    if (true)
                    {
                        foreach (var i in d)
                        {
                            if (i.Key == "ID")
                            {
                                if(!Validation.ValidateId(i.Value.ToString()).Item2)
                                    throw new Exception($"Validation Error! Please change value {i.Value.ToString()} for correct work  ");
                                continue;
                            }
                            if (i.Key == "URL")
                            {
                                if (!Validation.ValidateURL(i.Value.ToString()).Item2)
                                    throw new Exception($"Validation Error! Please change value {i.Value.ToString()} for correct work  ");
                                continue;
                            }
                            if (i.Key == "StartDate")
                            {
                                if (!Validation.ValidateDate(i.Value.ToString()).Item2)
                                    throw new Exception($"Validation Error! Please change value {i.Value.ToString()} for correct work  "); ;
                                continue;
                            }
                            if (i.Key == "EndDate")
                            {
                                if (!Validation.ValidateDate(i.Value.ToString()).Item2)
                                    throw new Exception($"Validation Error! Please change value {i.Value.ToString()} for correct work  ");
                                continue;
                            }
                            if (i.Key == "Price")
                            {
                                if (!Validation.ValidatePrice(i.Value.ToString()).Item2)
                                    throw new Exception($"Validation Error! Please change value {i.Value.ToString()} for correct work  ");
                                continue;
                            }
                            if (i.Key == "PhotoURL")
                            {
                                if (!Validation.ValidateURL(i.Value.ToString()).Item2)
                                    throw new Exception($"Validation Error! Please change value {i.Value.ToString()} for correct work  "); 
                                continue;
                            }
                            if (i.Key == "TransactionNumber")
                            {
                                if (!Validation.ValidateTransactionNumber(i.Value.ToString()).Item2)
                                    throw new Exception($"Validation Error! Please change value {i.Value.ToString()} for correct work  ");
                                continue;
                            }
                        }
                    }
                    dict.Add(item);
                    model.Add(d);
                }

                if (printFileToConsole)
                WriteConsoleDictionary(model);

                return model;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static Dictionary<string, object> ConvertToDictionary(string jsonData)
        {
            var val = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);
            var val2 = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> dict in val)
            {
                if (dict.Value is JObject)
                {
                    val2.Add(dict.Key, ConvertToDictionary(dict.Value.ToString()));
                }
                else
                {
                    val2.Add(dict.Key, dict.Value);
                }
            }
            return val2;
        }

        private static void WriteConsoleDictionary(List<Dictionary<string, object>> inputModel)
        {
            Console.WriteLine($"Import file - count of rows: {inputModel.Count}");
            string s = String.Empty;
            foreach (var item in inputModel.Take(1))
            {
                foreach (KeyValuePair<string, object> kvp in item)
                {
                    s = String.Join("  ", s, kvp.Key);
                }
            }
            Console.WriteLine(s);
            foreach (var item in inputModel)
            {
                foreach (KeyValuePair<string, object> kvp in item)
                {
                    Console.Write(kvp.Value + "  ");
                }
                Console.Write("\t\n");
            }
        }
    }
}
