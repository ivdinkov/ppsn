using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace PPSN_Validator
{
    class Program
    {
        static void Main(string[] args)
        {
            var valid = Validate("1234567AA");

            Console.WriteLine(valid);
            Console.ReadLine();
        }
        public static bool Validate(string ppsn)
        {
            const string PPSNREGEX = @"^(\d{7})([a-zA-Z]{1,2})$";

            if (string.IsNullOrEmpty(ppsn))
                return false;

            if (Regex.IsMatch(ppsn, PPSNREGEX))
            {
                int sum = 0;
                int factor = 8;
                int remainder = 0;
                string digitsToCheck = ppsn.Substring(0, 7);
                string checkSum = ppsn.Substring(7, ppsn.Count() - 7).ToUpper();
                string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

                CalculateSum(ref sum, ref factor, digitsToCheck);

                if (checkSum.Count() == 1)
                {
                    remainder = GetReminder(sum);

                    if (remainder == 0)
                    {
                        if (checkSum.Equals("W"))
                            return true;
                    }
                    else
                    {
                        var c = (Char)(65 + (remainder - 1));
                        if (c.ToString().Equals(checkSum))
                            return true;
                    }
                }
                else
                {
                    sum += (int)(alphabet.IndexOf(ppsn.Substring(ppsn.Count() - 1)) + 1) * 9;
                    remainder = sum % 23;

                    if (remainder == 0)
                    {
                        if (ppsn.Substring(ppsn.Count() - 2, 1).Equals("W"))
                            return true;
                    }
                    else
                    {
                        if (ppsn.Substring(ppsn.Count() - 2, 1).Equals(alphabet.Substring((int)remainder - 1, 1)))
                            return true;
                    }
                }
            }

            return false;
        }

        private static int GetReminder(int sum)
        {
            return sum % 23;
        }

        private static void CalculateSum(ref int sum, ref int factor, string digitsToCheck)
        {
            for (int i = 0; i < digitsToCheck.Count(); i++)
            {
                sum += int.Parse(digitsToCheck.Substring(i, 1)) * factor--;
            }
        }
    }
}
