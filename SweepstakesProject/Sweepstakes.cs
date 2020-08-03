using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesProject
{
    public class Sweepstakes
    {
        /// <summary>
        /// Dictionary containing all Registered Contestants.
        /// </summary>
        /// <remarks></remarks>
        Dictionary<int, Contestant> contestants;
        private string name;
        public string Name { get; set; }
        /// <summary>
        /// Constructor for Sweepstakes.
        /// </summary>
        /// <param name="name">Name of Sweepstakes</param>
        public Sweepstakes(string name)
        {
            this.name = name;
        }

        public void RegisterContestant(Contestant contestant)
        {
            contestants.Add(contestant.RegistrationNumber, contestant);
            // Throws exception if same contestant is entered twice.
        }
        public Contestant PickWinner()
        {
            //Add Logic to pick a winner.

            contestants.TryGetValue(1, out Contestant winner);
            // Placeholder returns first Contestant.
            return winner;
        }
        public void PrintContestantInfo(Contestant contestant)
        {
            // Send call to a physical printer.
            // Send call to UI.DisplayContestantInfo()
            UI.DisplayContestantInfo(contestant);

        }
            
    }
}
