using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesProject
{
    public class Contestant : ISweepstakesSubscriber
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }  //TODO: Look into Regex validation
        public int RegistrationNumber { get; private set; }

        //  Placeholder
        public Contestant()
        {
            throw new NotSupportedException("Contestant UI frontend not implemented currently.");
            RegisterContestant();
            GenerateRegistrationNumber();
        }

        public Contestant(string firstName, string lastName, string emailAddress, int registrationNumer)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            RegistrationNumber = registrationNumer;  // MVP- generation/validation post 
        }



        private void GenerateRegistrationNumber()
        {
            throw new NotImplementedException("Contestant UI frontend not implemented currently.");
        }

        public void RegisterContestant()
        {
            throw new NotImplementedException("Contestant UI frontend not implemented currently.");
        }

        public void Notify(string sweepstakesName, Contestant winner)
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"{FirstName} has been notified that {winner.FirstName} won the {sweepstakesName} Sweepstakes!");
            Console.ResetColor();
        }
    }
}
