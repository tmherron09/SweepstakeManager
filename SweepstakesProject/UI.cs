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


    }
}
