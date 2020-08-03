using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SweepstakesProject
{
    /// <summary>
    /// Class representing a contestant that can register for a Sweepstakes. Subscribes to Sweepstakes and recieves notifications about winners or if the contestant has won, a notification from the Marketing Firm.
    /// </summary>
    [Serializable]
    public class Contestant : ISweepstakesSubscriber
    {
        /// <summary>
        /// First name of Contestant
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of Contestant
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Email Address provided to send communications to, including notification of end of sweepstakes, winners and occasional marketing information and surveys,
        /// </summary>
        public string EmailAddress { get; set; }  //TODO: Look into Regex validation
        /// <summary>
        /// Registration number of Contestant in a Sweepstakes.
        /// </summary>
        public int RegistrationNumber { get; private set; }

        /// <summary>
        /// Constructor for a new Contestant for Registration in Sweepstakes.
        /// </summary>
        /// <param name="firstName">Contestant first name.</param>
        /// <param name="lastName">Contesetant last name.</param>
        /// <param name="emailAddress">Email Address provided by Contestant.</param>
        /// <param name="registrationNumer">Registration number of Contestant in a Sweepstakes.</param>
        public Contestant(string firstName, string lastName, string emailAddress, int registrationNumer)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            RegistrationNumber = registrationNumer;  // MVP- generation/validation post 
        }
        /// <summary>
        /// Using the Observer pattern, Contestant is notified when Sweepstakes ends and winner of Sweepstakes. Contestant will be contacted directly by Marketing Firm upon winner and will not recieve general notification.
        /// </summary>
        /// <param name="sweepstakesName">Name of Sweepstakes Contestant is registered in.</param>
        /// <param name="winner">Winning Contestant.</param>
        public void Notify(string sweepstakesName, Contestant winner)
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"{FirstName} has been notified that {winner.FirstName} won the {sweepstakesName} Sweepstakes!");
            Console.ResetColor();
        }
    }
}
