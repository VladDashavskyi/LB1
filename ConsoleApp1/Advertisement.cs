using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace App
{
    public static class Advertisement
    {

        public static string[] ReadFile()
        {
            List<InputModel> outList = new List<InputModel>();
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

        public static List<InputModel> ParceFileToModel(string[] file)
        {
            List<InputModel> outList = new List<InputModel>();
            try
            {
                foreach (var line in file)
                {
                    string[] columns = line.Split(new char[] { ',' });

                    InputModel inputModel = new InputModel
                    {
                        ID = Validation.ValidateId(columns[0]),
                        URL = Validation.ValidateURL(columns[1]),
                        StartDate = Validation.ValidateDate(columns[2]),
                        EndDate = Validation.ValidateDate(columns[3]),
                        Price = Validation.ValidatePrice(columns[4]),
                        Title = Validation.ValidateTitle(columns[5]),
                        PhotoURl = Validation.ValidateURL(columns[6]),
                        TransactionNumber = Validation.ValidateTransactionNumber(columns[7])
                    };

                    outList.Add(inputModel);
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

        public static void WriteFile(List<InputModel> inputModels)
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"out\outFile.csv");

            using (var stream = File.AppendText(file))
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
            return input;


        }

        public static void AddRowFile(int id, string url, DateTime startDate, DateTime endDate, decimal price, string title, string photoURl, string transactionNumber)
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"in\outFile.csv");

            using (var stream = File.AppendText(file))
            {
                string csvRow;

                csvRow = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                    id,
                    url,
                    startDate,
                    endDate,
                    price,
                    title,
                    photoURl,
                    transactionNumber
                 );

                stream.WriteLine(csvRow);
            }
        }

        public static List<InputModel> DeleteRow(List<InputModel> input, int id)
        {
            var delRow = input.FirstOrDefault(i => i.ID == id);
            input.Remove(delRow);

            return input;
        }

        public static List<InputModel> UpdateRow(List<InputModel> input,int id, string property, string value)
        {
            var newInput = input.Where(w => w.ID == id).ToList();
            newInput.Price = 555;

            return newInput;
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
                input.Where(w => w.ID == id);
            }
            //var res = input.Where(w => w.GetType().GetProperty(property).Name == w.GetType().GetProperty(property).GetValue(value,null));

            //var valuefind = input.FindAll(x =>
            //{
            //    var dic = x as InputModel;


            //    return dic.GetType().GetProperties().Any(key => dic.GetType().GetProperty(property).ToString().Contains(value));

            //};
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
            
  