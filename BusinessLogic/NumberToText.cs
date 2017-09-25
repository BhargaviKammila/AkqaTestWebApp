using BusinessContract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    [Export(typeof(INumberToText))]
    public class NumberToText : INumberToText
    {
        #region "Private"

        private string CalculateNumber(double n)
        {
            string[] numbersArr = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            string[] tensArr = new string[] { "twenty", "thirty", "fourty", "fifty", "sixty", "seventy", "eighty", "ninty" };
            string[] suffixesArr = new string[] { "thousand", "million", "billion", "trillion", "quadrillion", "quintillion", "sextillion" };
            string words = "";

            bool tens = false;

            if (n < 0)
            {
                words += "negative ";
                n *= -1;
            }

            int power = (suffixesArr.Length + 1) * 3;

            while (power > 3)
            {
                double pow = Math.Pow(10, power);
                if (n >= pow)
                {
                    if (n % pow > 0)
                    {
                        words += CalculateNumber(Math.Floor(n / pow)) + " " + suffixesArr[(power / 3) - 1] + " and ";
                    }
                    else if (n % pow == 0)
                    {
                        words += CalculateNumber(Math.Floor(n / pow)) + " " + suffixesArr[(power / 3) - 1];
                    }
                    n %= pow;
                }
                power -= 3;
            }
            if (n >= 1000)
            {
                if (n % 1000 > 0) words += CalculateNumber(Math.Floor(n / 1000)) + " thousand and ";
                else words += CalculateNumber(Math.Floor(n / 1000)) + " thousand ";
                n %= 1000;
            }
            if (0 <= n && n <= 999)
            {
                if ((int)n / 100 > 0)
                {
                    words += CalculateNumber(Math.Floor(n / 100)) + " hundred and ";
                    n %= 100;
                }
                if ((int)n / 10 > 1)
                {
                    if (words != "")
                        words += " ";
                    words += tensArr[(int)n / 10 - 2];
                    tens = true;
                    n %= 10;
                }

                if (n < 20 && n > 0)
                {
                    if (words != "" && tens == false)
                        words += " ";
                    words += (tens ? "-" + numbersArr[(int)n - 1] : numbersArr[(int)n - 1]);
                    n -= Math.Floor(n);
                }
            }

            return words;

        }

        private string NumberWrapper(double n)
        {
            string words = "";
            double intPart;
            double decPart = 0;
            if (n == 0)
                return "zero";
            try
            {
                string[] splitter = n.ToString().Split('.');
                intPart = double.Parse(splitter[0]);
                decPart = double.Parse(splitter[1]);
            }
            catch
            {
                intPart = n;
            }

            words = CalculateNumber(intPart);

            if (decPart > 0)
            {
                if (words != "")
                    words += " dollars and ";

                words += CalculateNumber(decPart) + " cents";
            }
            else
            {
                words += " dollars ";
            }
            return words.ToUpper();
        }

        #endregion

        #region "Public"

        public string ConvertNumberToText(double number)
        {
            try
            {
                return NumberWrapper(number);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        #endregion "Public"
    }
}
