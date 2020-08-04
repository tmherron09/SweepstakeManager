using System;

namespace SweepstakesProject
{
    class Program
    {
        static void Main(string[] args)
        {
            // These values do not affect the program.
            Console.SetWindowSize(80, 30);
            Console.BufferWidth = 80;

            new Simulation().Run();


            

            Console.ReadLine();
        }
    }
}
