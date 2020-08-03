using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesProject
{
    public static class UI
    {
        public static string GetInputFor(string prompt)
        {
            Console.WriteLine(prompt);

            return Console.ReadLine();

        }

        public static bool GetInputYesNo(string prompt)
        {
            Console.WriteLine(prompt);
            string input = UI.GetInputFor("(y/n)").ToLower();
            if( input == "y" || input == "yes")
            {
                return true;
            }
            else if (input == "n" || input == "no")
            {
                return false;
            }
            else
            {
                return UI.GetInputYesNo(prompt);
            }
        }

        public static void DisplayText(string text)
        {
            Console.WriteLine(text);
        }

    }
}
