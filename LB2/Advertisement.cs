using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace LB2
{
    public static class Advertisement
    {

        public static string[] ReadFile()
        {
            try
            {
                var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"in\Input.csv");
                return File.ReadAllLines(file);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static List<InputModel> ParceFileToModel()
        {
            try
            {
                List<InputModel> outList = new List<InputModel>();

                var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"in\Input.json");
                using (StreamReader r = new StreamReader(file))
                {
                    string json = r.ReadToEnd();
                    outList = JsonConvert.DeserializeObject<List<InputModel>>(json);
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

        public static void WriteToJsonFile(List<InputModel> inputModels)
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"in\Input.json");
            using (StreamWriter newFile = File.CreateText(file))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(newFile, inputModels);
            }
        }

        public static void WriteFile(List<InputModel> inputModels)
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"in\Input.csv");

            File.Delete(file);

            using (var stream = File.CreateText(file))
            {
                string csvRow;

                int? ID = null;
                string URL, title, photoURl, transactionNumber;
                DateTime? startDate, endDate;
                decimal? price;

                foreach (var row in inputModels)
                {
                    csvRow = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                        ID = row.ID == null ? null : (int?)row.ID.Value,
                        URL = row.URL,
                        startDate = row.StartDate,
                        endDate = row.EndDate,
                        price = row.Price,
                        title = row.Title,
                        photoURl = row.PhotoURl,
                        transactionNumber = row.TransactionNumber
                     );

                    stream.WriteLine(csvRow);
                }
            }
        }

        public static List<InputModel> AddRow(List<InputModel> input, string id, string url, string startDate, string endDate, string price, string title, string photoURl, string transactionNumber)
        {

            InputModel addNewRow = new InputModel
            {

                ID = Validation.ValidateId(id),
                URL = Validation.ValidateURL(url),
                StartDate = Validation.ValidateDate(startDate),
                EndDate = Validation.ValidateDate(endDate),
                PhotoURl = Validation.ValidateURL(photoURl),
                Price = Validation.ValidatePrice(price),
                Title = Validation.ValidateTitle(title),
                TransactionNumber = Validation.ValidateTransactionNumber(transactionNumber)
            };

            input.Add(addNewRow);
            WriteFile(input);
            return input;


        }
        public static List<InputModel> DeleteRow(List<InputModel> input, int id)
        {
            var delRow = input.FirstOrDefault(i => i.ID == id);
            input.Remove(delRow);

            WriteToJsonFile(input);
            return input;
        }

        public static List<InputModel> UpdateRow(List<InputModel> input,string updateId, string property, string value)
        {
            int.TryParse(updateId, out int id);
            foreach (var row in input.Where(w => w.ID == id))
            {
                if(property == "URL")
                {
                    row.URL = Validation.ValidateURL(value);
                }
                if(property == "Price")
                {
                    row.Price = Validation.ValidatePrice(value);
                }
                if(property == "StartDate")
                {
                    row.StartDate = Validation.ValidateDate(value);
                }

            }
            WriteFile(input);
            return input;
        }

        public static List<InputModel> Sort(List<InputModel> input, string property)
        {
            return input.OrderByDescending(p => p.GetType()
                                       .GetProperty(property)
                                       .GetValue(p, null)).ToList();

        }

        public static List<InputModel> Filter(List<InputModel> input, string property, string value)
        {
            if (property == "ID")
            {
                int.TryParse(value, out int id);
                return input.Where(w => w.ID == id).ToList();
            }
            if (property == "URL")
            {
                return input.Where(w => w.URL.Contains(value)).ToList();
            }
            if (property == "StartDate")
            {
                var date = Validation.ValidateDate(value);
                return input.Where(w => w.StartDate == date).ToList();
            }
            if (property == "EndDate")
            {
                var date1 = Validation.ValidateDate(value);
                return input.Where(w => w.EndDate == date1).ToList();
            }
            if (property == "Price")
            {
                var price = Validation.ValidatePrice(value);
                return input.Where(w => w.Price == price).ToList();
            }
            if (property == "Title")
            {
                return input.Where(w => w.Title.Contains(value)).ToList();
            }
            if (property == "PhotoURL")
            {
                return input.Where(w => w.PhotoURl.Contains(value)).ToList();
            }
            if (property == "TransactionNumber")
            {
                return input.Where(w => w.TransactionNumber == value).ToList();
            }

            return new List<InputModel>();
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
            
  