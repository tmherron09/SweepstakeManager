using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(80, 30);
            Console.BufferWidth = 80;

            //new Simulation().Start();
            MarketingFirm mk = new MarketingFirm(new SweepstakesStackManager());
            mk.CreateSweepstakes();

            Sweepstakes swst = mk._manager.GetSweepstakes();
            Contestant tim = new Contestant("Tim", "Herron", "tmherron09@gmail.com", 1);
            Contestant jeff = new Contestant("Jeff", "Gimm", "jgimm@gmail.com", 2);
            Contestant elle = new Contestant("Elle", "Gimm", "writerperson@gmail.com", 3);
            Contestant jake = new Contestant("Jake", "Kaufmann", "jakey@gmail.com", 4);
            Contestant myra = new Contestant("Myra", "Krusemark", "mk@gmail.com", 5);

            swst.RegisterContestant(tim);
            swst.RegisterContestant(jeff);
            swst.RegisterContestant(elle);
            swst.RegisterContestant(jake);
            swst.RegisterContestant(myra);

            mk.SweepstakesEmployeeMenu();

            //swst.PrintContestantInfo(tim);
            //swst.PrintContestantInfo(jeff);
            //swst.PrintContestantInfo(elle);
            //swst.PrintContestantInfo(jake);
            //swst.PrintContestantInfo(myra);


            Console.ReadLine();
        }
    }
}
