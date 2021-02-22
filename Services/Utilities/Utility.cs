using System;
using System.Text.RegularExpressions;

namespace StudentApp.Services.Utilities
{
    public static class Utility
    {
        public static string GetStringInput(string regex, string helpText)
        {
            Console.WriteLine(helpText);
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(regex) && Regex.IsMatch(input, regex))
            {
                return input;
            }
            Console.WriteLine("Invalid input");

            return GetStringInput(regex, helpText);
        }

        public static int GetIntInput(string helpText)
        {
            try
            {
                Console.WriteLine(helpText);
                int integerInput = Convert.ToInt32(Console.ReadLine());
                if (integerInput >= 0)
                {
                    return integerInput;
                }
                Console.Write("Invalid input");

                return GetIntInput(helpText);
            }
            catch (Exception)
            {
                return GetIntInput(helpText);
            }
        }
    }
}