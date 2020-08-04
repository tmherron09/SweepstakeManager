using System;
using System.IO;

namespace SweepstakesProject
{
    /// <summary>
    /// Class to Run main logic of Sweepstakes Manager
    /// </summary>
    public class Simulation
    {
        /// <summary>
        /// The Marketing Firm currently using the Sweepstakes Manager
        /// </summary>
        MarketingFirm marketingFirm;

        /// <summary>
        /// Main Application Logic. Call this method to start Sweepstakes Manager
        /// </summary>
        private void Run()
        {
            WelcomeMessage();

            marketingFirm = LoadMarketingFirm();

            CreateMarketingFirmWithManager();

            AddSweepstakesToCampaign();

            Menu();

        }
        /// <summary>
        /// Displays a welcome message to the user.
        /// </summary>
        public void WelcomeMessage()
        {
            string welcome = "Welcome to the Sweepstakes Manager Application.\nPress any key to continue...";
            UI.DisplayText(welcome);
            Console.ReadKey();

        }
        /// <summary>
        /// Displays the Main Menu of the Sweepstakes Manager. User can access current sweepstakes information, add new sweepstakes, save their Marketing Firm Data or call to Exit Program.
        /// </summary>
        private void Menu()
        {
            string prompt = "Please select an option:\n1) [V]iew Current Sweepstakes\n2) Add [N]ew Sweepstakes\n3) [S]ave Marketing Firm data to file\n4) [E]xit Program";
            string input = UI.GetInputFor(prompt);
            switch (input.ToLower())
            {
                case "v":
                case "view current sweepstakes":
                case "1":
                    
                    Menu();
                    break;
                case "n":
                case "add new sweepstakes":
                case "2":
                    AddSweepstakesToCampaign()
                    Menu();
                    break;
                case "s":
                case "save marketing firm data to file":
                case "3":
                    SaveMarketingFirm();
                    Menu();
                    break;
                case "e":
                case "exit program":
                case "4":
                    UI.DisplayText("Thank you and have a pleasant day.");
                    break;
                default:
                    UI.DisplayText("Invalid Selection.");
                    Menu();
                    break;
            }
        }
        /// <summary>
        /// Allows user to add a new Sweepstakes to their Campaign/ Sweepstakes Manager.
        /// </summary>
        private void AddSweepstakesToCampaign()
        {
            while (UI.GetInputYesNo("Would you like to add a another Sweepstakes to your campaign?"))
            {
                marketingFirm.CreateSweepstakes();
            }
        }
        /// <summary>
        /// Creates a new Marketing Firm instance using Dependency Injection with the ISweepstakesManager class. All Marketing Firms have access to the same methods and abilities, DI allows the underlying logic to be hidden/not needed to be known, by the user.
        /// </summary>
        public void CreateMarketingFirmWithManager()
        {
            if (marketingFirm != null) return;
            string marketingFirmName = UI.GetInputFor("Please enter the name of your Marketing Firm:");
            string campaignType = UI.GetInputFor("Please Select a Sweepstakes Campaign Type:\n1) Annual\n2) Promotion");
            // Use of Dependency Injection to create a new SweepstakesManager. Marketing Firm doesn't know the underlying data structor or how of the code. It just knows it has access to the same methods regardless of what type of Sweepstakes Manager is injected.
            ISweepstakeManager sweepstakeManager = CreateSweepstakesManager(campaignType);
            marketingFirm = new MarketingFirm(sweepstakeManager, marketingFirmName, "UNUSED"); 
        }

        /// <summary>
        /// Returns an ISweepstakesManager instance containing any implementing SweepstakesManagers.
        /// </summary>
        /// <param name="campaignType">Input from CreateMarketingFirmWithManager.</param>
        /// <returns>ISweepstakes Manager Instance</returns>
        public ISweepstakeManager CreateSweepstakesManager(string campaignType)
        {
            ISweepstakeManager sweepstakeManager;

            switch (campaignType.ToLower())
            {
                case "1":
                case "annual":
                    sweepstakeManager = new SweepstakesQueueManager();
                    break;
                case "2":
                case "promo":
                case "promotion":
                    sweepstakeManager = new SweepstakesStackManager();
                    break;
                default:
                    return CreateSweepstakesManager(UI.GetInputFor("Invalid Selection\nPlease Select a Sweepstakes Campaign Type:\n1) One-Time\n2)"));
            }
            return sweepstakeManager;
        }
        /// <summary>
        /// Calls a BinaryFormatter to save the current Marketing Firm Data to a *.bin file. No validation checks are made by the SweepstakesManager, user is expected to insert a valid name.
        /// </summary>
        public void SaveMarketingFirm()
        {
            string fileName = UI.GetInputFor("Please enter a file name (CAUTION NO VALIDATION IMPLEMENTED, USE LEGAL FILE NAMES ONLY)");
            using (Stream stream = File.Open($"{fileName}.bin", FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, marketingFirm);
            }

        }
        /// <summary>
        /// Loads a Marketing Firm from a *.bin file previously saved by the Sweepstakes Manager program. Exact name of file must be known by user and placed in same directory as the executable file.
        /// </summary>
        /// <returns></returns>
        public MarketingFirm LoadMarketingFirm()
        {
            MarketingFirm marketingFirm = null;

            if (UI.GetInputYesNo("Do you have a previous Marketing Firm file you'd like to load?"))
            {
                string fileName = UI.GetInputFor("Please enter filename (without *.bin):");

                if (File.Exists($"{fileName}.bin"))
                {
                    using (Stream stream = File.Open($"{fileName}.bin", FileMode.Open))
                    {
                        var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        marketingFirm = (MarketingFirm)binaryFormatter.Deserialize(stream);
                    }
                }
                else
                {
                    UI.DisplayText("We're sorry, that is an invalid file name.");
                    marketingFirm = LoadMarketingFirm();
                }
            }
            return marketingFirm;
        }


    }
}
