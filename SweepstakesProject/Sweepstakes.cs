using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SweepstakesProject
{
    /// <summary>
    /// Represents an instance of a Marketing Firm's Sweepstakes.
    /// </summary>
    [Serializable]
    public class Sweepstakes : IEnumerable
    {
        /// <summary>
        /// Dictionary containing all Registered Contestants.
        /// </summary>
        /// <remarks><c>Dictionary&lt;Contestant Registration Number, Contestant Info&gt;</c></remarks>
        Dictionary<int, Contestant> contestants;
        /// <summary>
        /// List of Subscribers to the Sweepstakes.
        /// </summary>
        public List<ISweepstakesSubscriber> sweepstakesSubscribers;
        /// <summary>
        /// Name of Sweepstakes.
        /// </summary>
        private string name;
        /// <summary>
        /// Name of Sweepstakes.
        /// </summary>
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
            sweepstakesSubscribers = new List<ISweepstakesSubscriber>();
        }
        /// <summary>
        /// Registers a contestant into the Sweepstakes.
        /// </summary>
        /// <param name="contestant">Contestant to Register.</param>
        public void RegisterContestant(Contestant contestant)
        {
            contestants.Add(contestant.RegistrationNumber, contestant);
            Subscribe(contestant);
        }
        /// <summary>
        /// **Used when Not Emailing. Randomly selects a Winning Contestant that has registered this this Sweepstakes instance.
        /// </summary>
        /// <returns>The Winning Contestant</returns>
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
        /// <summary>
        /// Randomly selects a Winning Contestant that has registered this this Sweepstakes instance. Starts the call to all Subscribers of this Sweepstakes to be notified of the winner.
        /// </summary>
        /// <param name="nextSweepstakesName">Name of Next Sweepstakes in the Sweepstakes Manager to be passed into Notifications.</param>
        public void PickWinner(string nextSweepstakesName)
        {
            Random rng = new Random(DateTime.Now.Second);

            int winningRegistrationNumber;
            do
            {
                winningRegistrationNumber = rng.Next(1, contestants.Count * 10_000) % contestants.Count;

            } while (!contestants.ContainsKey(winningRegistrationNumber));
            Contestant winner = contestants[winningRegistrationNumber];

            AnnounceWinner(winner, nextSweepstakesName);
        }
        /// <summary>
        /// **Used when Not Emailing. Intermediary Step in Announcing Winners. Displays Winner to Console for user visual.
        /// </summary>
        /// <param name="winner">Winning Contestant</param>
        public void AnnounceWinner(Contestant winner)
        {
            PrintContestantInfo(winner);
            UI.DisplayText($"Congratulations to {winner.FirstName} {winner.LastName}!");
            NotifyContestSubscribers(winner);
        }
        /// <summary>
        /// Intermediary Step in Announcing Winners. Displays Winner to Console for user visual.
        /// </summary>
        /// <param name="winner">Winning Contestant</param>
        /// <param name="nextSweepstakesName">Name of Next Sweepstakes in the Sweepstakes Manager to be passed into Notifications.</param>
        public void AnnounceWinner(Contestant winner, string nextSweepstakesName)
        {
            PrintContestantInfo(winner);
            UI.DisplayText($"Congratulations to {winner.FirstName} {winner.LastName}!");
            NotifyContestSubscribers(winner, nextSweepstakesName);
        }
        /// <summary>
        /// Prints a Contestants info to the Console.
        /// </summary>
        /// <param name="contestant"></param>
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
        /// <summary>
        /// Allows a contest to be iterated over by their containing Contestant objects.
        /// </summary>
        /// <returns>Next Contestant in Sweepstakes.</returns>
        public IEnumerator GetEnumerator()
        {
            foreach (Contestant contestant in contestants.Values)
            {
                yield return contestant;
            }
        }
        /// <summary>
        /// Adds Subscriber to Sweepstakes.
        /// </summary>
        /// <param name="contestSubscriber">Subscriber of Sweepstakes</param>
        public void Subscribe(ISweepstakesSubscriber contestSubscriber)
        {
            sweepstakesSubscribers.Add(contestSubscriber);
        }
        /// <summary>
        /// **Used when Not Emailing. Notifys all Contest Subscribers. Direc
        /// </summary>
        /// <param name="winner">Winning Contestant</param>
        private void NotifyContestSubscribers(Contestant winner)
        {
            foreach (ISweepstakesSubscriber subscriber in sweepstakesSubscribers)
            {
                if (subscriber != winner)
                {
                    //subscriber.Notify(Name, winner);
                }
            }
        }
        /// <summary>
        /// Notifys all subscribers of the End of the Sweepstakes. Does not directly notify winner, Marketing Firm directly notifys the winner.
        /// </summary>
        /// <param name="winner"></param>
        /// <param name="nextSweepstakesName"></param>
        private void NotifyContestSubscribers(Contestant winner, string nextSweepstakesName)
        {
            foreach (ISweepstakesSubscriber subscriber in sweepstakesSubscribers)
            {
                if (subscriber != winner)
                {
                    subscriber.Notify(Name, nextSweepstakesName, winner);
                }
            }
        }
    }
}
