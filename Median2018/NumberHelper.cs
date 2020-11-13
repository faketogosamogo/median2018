using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Median2018
{
    public static class NumberHelper
    {
        public static long GetNumberFromString(string strNumber)
        {
            strNumber =  Regex.Replace(strNumber, @"[^\d]", "");

            long number = Convert.ToInt64(strNumber);
            return number;
        }
        public static string GetStringFromNumber(int number)
        {
            return (number.ToString() + "\n");
        }
        public static double GetMedian(long[] numbers)
        {
            int len = numbers.Length;
            Array.Sort(numbers);
          
            if (numbers.Count() % 2 == 1) return numbers[len / 2];
            return 0.5 * (numbers[(len / 2) - 1] + numbers[len / 2]);            
        } 
    }
}
