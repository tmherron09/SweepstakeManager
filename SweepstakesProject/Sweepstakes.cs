using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesProject
{
    public class Sweepstakes : IEnumerable
    {
        /// <summary>
        /// Dictionary containing all Registered Contestants.
        /// </summary>
        /// <remarks><c>Dictionary&lt;Contestant Registration Number, Contestant Info&gt;</c></remarks>
        Dictionary<int, Contestant> contestants;
        private string name;
        public string Name { 
            get
            {
                return name;
            }
            private set
            {
                name = value;
            }
        }
        public int NextRegistrationNumber {
            get
            {
                return contestants.Count + 1;
            }
        }
        /// <summary>
        /// Constructor for Sweepstakes.
        /// </summary>
        /// <param name="name">Name of Sweepstakes</param>
        public Sweepstakes(string name)
        {
            this.name = name;
            contestants = new Dictionary<int, Contestant>();
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
            StringBuilder sb = new StringBuilder();
            sb.Append(new string('▒', Console.BufferWidth));
            sb.AppendLine($"Contestant Info\nRegistration #{contestant.RegistrationNumber}");
            sb.AppendLine($"Last Name: {contestant.LastName}");
            sb.AppendLine($"First Name: {contestant.FirstName}");
            sb.AppendLine($"Email Address: {contestant.EmailAddress}");
            sb.Append(new string('▒', Console.BufferWidth));
            UI.DisplayText(sb.ToString());
        }

        public IEnumerator GetEnumerator()
        {
            foreach(Contestant contestant in contestants.Values)
            {
                yield return contestant;
            }
        }
    }
}
