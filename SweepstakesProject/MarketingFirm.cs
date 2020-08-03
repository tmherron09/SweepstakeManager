using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesProject
{
    public class MarketingFirm : ISweepstakesSubscriber
    {
        /// <summary>
        /// Marketing Manager using Dependency Injection of the ISweepstakesManager interface.
        /// </summary>
        public ISweepstakeManager _manager;
        /// <summary>
        /// Name of Marketing Firm. *If provided.
        /// </summary>
        public string marketingFirmName { get; set; }
        /// <summary>
        /// Name of campaign (Collection of Sweepstakes.) *If provided.
        /// </summary>
        public string campaignName { get; set; }
        /// <summary>
        /// Constructs a new Marketing Firm using Dependency Injection. The Marketing firm does not need to know the underlying structure of the Sweepstakes manager, rather that all Marketing Firms can use Sweepstakes managers in the same manner.
        /// </summary>
        /// <param name="sweepstakeManager"></param>
        public MarketingFirm(ISweepstakeManager sweepstakeManager)
        {
            _manager = sweepstakeManager;
        }
        // TODO if implementing current campaign structure.
        public MarketingFirm(ISweepstakeManager sweepstakeManager, string markeringFirmName,  string campaignName)
        {
            _manager = sweepstakeManager;
            this.marketingFirmName = marketingFirmName;
            this.campaignName = campaignName;
        }
        /// <summary>
        /// Creates and inserts a new Sweepstakes into the Sweepstakes manager's underlying data structure.
        /// </summary>
        public void CreateSweepstakes()
        {
            string sweepstakesName;
            do
            {
                sweepstakesName = UI.GetInputFor("Please enter the name for the Sweepstakes:");
                UI.DisplayText(sweepstakesName);
            } while (!UI.GetInputYesNo("Are you satisfied with this name?"));

            Sweepstakes sweepstakes = new Sweepstakes(sweepstakesName);
            _manager.InsertSweepstakes(sweepstakes);
            sweepstakes.Subscribe(this);
            UI.DisplayText($"New Sweepstakes {sweepstakes.Name} has been successfully added!");
        
        }
        public void SweepstakesEmployeeMenu()
        {
            if (_manager.GetSweepstakes() == null)
            {
                UI.DisplayText("No Sweepstakes avilable. Please return to main menu and Create new Sweepstakes.\nPress any key...");
                Console.ReadKey();
                return;
            }
            Sweepstakes currentSweepstakes = _manager.GetSweepstakes();
            UI.DisplayText($"Current Sweepstakes: {currentSweepstakes.Name}");
            string prompt = "Please select an option:\n1) [R]egister New Contestant\n2) [D]isplay Contestant Info\n3) [E]nd Sweepstakes \n4) Return to [M]ain Menu";
            string input = UI.GetInputFor(prompt);
            switch (input.ToLower())
            {
                case "r":
                case "register new contestant":
                case "1":
                    RegisterNewContestant(currentSweepstakes);
                    SweepstakesEmployeeMenu();
                    break;
                case "d":
                case "display contestant info":
                case "2":
                    PrintAllContestantInfo();
                    SweepstakesEmployeeMenu();
                    break;
                case "3":
                    _manager.EndSweepstakes();
                    break;
                case "m":
                case "return to main menu":
                case "4":
                    break;
                default:
                    UI.DisplayText("Invalid Selection.");
                    SweepstakesEmployeeMenu();
                    break;
            }
        }

        private void RegisterNewContestant(Sweepstakes currentSweepstakes)
        {
            string firstName = UI.GetInputFor("Please enter contestant's first name:");
            string lastName = UI.GetInputFor("Please enter contestant's last name:");
            string emailAddress = UI.GetInputFor("Please enter contestant's email address:");
            Contestant registrant = new Contestant(firstName, lastName, emailAddress, currentSweepstakes.NextRegistrationNumber);
            currentSweepstakes.RegisterContestant(registrant);
            // Have registrant subscribe to Marketing firm to recieve email blast via IObserver pattern.

        }
        private void PrintAllContestantInfo()
        {
            foreach(Contestant contestant in _manager.GetSweepstakes())
            {
                _manager.GetSweepstakes().PrintContestantInfo(contestant);

            }
        }

        public void Notify(string sweepstakesName, Contestant winner)
        {
            // Send special email to Winner!
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"Congratulations {winner.FirstName} {winner.LastName}!!! You are the big winner of our {sweepstakesName} Sweepstakes!\n To Claim your prize please contact us ASAP!");
            Console.ResetColor();
        }
    }
}
