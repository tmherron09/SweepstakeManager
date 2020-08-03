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
        public List<ISweepstakesSubscriber> contestSubscribers;
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                name = value;
            }
        }
        /// <summary>
        /// Value of next available Registration Number for a creating a contestant.
        /// </summary>
        public int NextRegistrationNumber
        {
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
            Random rng = new Random(DateTime.Now.Second);

            int winningRegistrationNumber;
            do
            {
                winningRegistrationNumber = rng.Next(1, contestants.Count * 10_000) % contestants.Count;

            } while (!contestants.ContainsKey(winningRegistrationNumber));
            Contestant winner = contestants[winningRegistrationNumber];

            AnnounceWinner(winner);

            return winner;
        }
        public void AnnounceWinner(Contestant winner)
        {
            PrintContestantInfo(winner);
            UI.DisplayText($"Congratulations to {winner.FirstName} {winner.LastName}!");
            NotifyContestSubscribers(winner);
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
            foreach (Contestant contestant in contestants.Values)
            {
                yield return contestant;
            }
        }
        public void Subscribe(ISweepstakesSubscriber contestSubscriber)
        {
            contestSubscribers.Add(contestSubscriber);
        }
        private void NotifyContestSubscribers(Contestant winner)
        {
            foreach(ISweepstakesSubscriber subscriber in contestSubscribers)
            {
                if(subscriber != winner)
                {
                    subscriber.Notify(Name, winner);
                }
            }
        }
    }
}
