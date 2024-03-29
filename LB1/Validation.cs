﻿using System;
using System.Text.RegularExpressions;

namespace App
{
    public static class Validation
    {
        public static int ValidateId(string id)
        {
            int result = 0;

            if (!int.TryParse(id, out result))
            {
                throw new Exception("ValidationError: Id - is not valid");
            }

            return result;
        }
        public static string ValidateURL(string url)
        {
            if (url.Substring(0, 4) == ("https"))
            {
                throw new Exception("ValidationError: URL - is not valid");
            }

            return url.Trim();
        }
        public static DateTime ValidateDate(string date)
        {
            DateTime dateTime = DateTime.Now;
            if (!DateTime.TryParse(date, out dateTime))
            {
                throw new Exception("ValidationError: Date - is not valid");
            }

            return dateTime;
        }
        public static decimal ValidatePrice(string price)
        {
            Regex r = new Regex(@"^\d+(\.\d{2})?$");

            if(!r.IsMatch(price.Trim()))
            {
                throw new Exception("ValidationError: Price - is not valid");
            }
            
            return decimal.Parse(price);
        }
        public static string ValidateTransactionNumber(string transactionNumber)
        {
            Regex r = new Regex(@"^[A-Z]{2}\-\d{3}\-\b[A-Z]{2}\/\d{2}");
            if (!r.IsMatch(transactionNumber.Trim()))
            {
                throw new Exception("ValidationError: Tranasaction number - is not valid");
            }
            return transactionNumber.Trim();
        }
        
        public static string ValidateTitle(string title)
        {
            
            return title.Trim();
        }
    }

}
