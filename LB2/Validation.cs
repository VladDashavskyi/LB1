using System;
using System.Text.RegularExpressions;

namespace Lab2
{
    public static class Validation
    {
        public static (int,bool) ValidateId(string id)
        {
            
            int result = 0;

            if (!int.TryParse(id, out result))
            {              
                Console.WriteLine("ValidationError: Id - is not valid");
                return (result, false);
            }

            return (result,true);
        }
        public static (string,bool) ValidateURL(string url)
        {
            if (url.Substring(0, 5) != ("https"))
            {
                Console.WriteLine("ValidationError: URL - is not valid");
                return (url, false);
            }
            return (url, true);
        }
        public static (DateTime,bool) ValidateDate(string date)
        {
            DateTime dateTime = DateTime.Now;
            if (!DateTime.TryParse(date, out dateTime))
            {
                Console.WriteLine("ValidationError: Date - is not valid");
                return (dateTime, false);
            }
            return (dateTime,true);
        }
        public static (decimal,bool) ValidatePrice(string price)
        {
            Regex r = new Regex(@"^\d+(\.\d{2})?$");
            if(!r.IsMatch(price.Trim()))
            {
                Console.WriteLine("ValidationError: Price - is not valid");
                return (decimal.Parse("0"), false);
            }
            var s = price;
            return (decimal.Parse(price), true);
        }
        public static (string,bool) ValidateTransactionNumber(string transactionNumber)
        {
            Regex r = new Regex(@"^[A-Z]{2}\-\d{3}\-\b[A-Z]{2}\/\d{2}");
            if (!r.IsMatch(transactionNumber.Trim()))
            {
                Console.WriteLine("ValidationError: Transaction Number - is not valid");
                return (String.Empty, false);
            }
            return (transactionNumber.Trim(),true);
        }
        
        public static string ValidateTitle(string title)
        {          
            return title.Trim();
        }

        public static string ValidateName(string name)
        {
            if (name.Length >= 2)
            {
                Regex r = new Regex(@"^[a-zA-Z]+$");
                if (!r.IsMatch(name.Trim()))
                {
                    throw new Exception("ValidationError: Name - is not valid");
                }
                return name.Trim();
            }
            else
            {
                throw new Exception("ValidationError: Name - is less then 2 symbols");
            }
        }

        public static string ValidateEmail(string email)
        {
            Regex r = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!r.IsMatch(email.Trim()))
            {
                throw new Exception("ValidationError: Email - is not valid");
            }
            return email.Trim();

        }

        public static string ValidatePassword(string password)
        {
            Regex r = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*d)[a-zA-Zd]{8,}$");
            if (!r.IsMatch(password.Trim()))
            {
                throw new Exception("ValidationError: Password - is not valid");
            }
            return password.Trim();
        }
    }
}
