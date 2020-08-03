using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SweepstakesProject
{
    public class Simulation
    {
        MarketingFirm marketingFirm;
        public Simulation()
        {

        }


        public void Start()
        {
            //  SimulationInitialization(); Logic for loading/saving data.

            Run();

        }

        private void SimulationInitialization()
        {
            throw new NotImplementedException();
        }

        private void Run()
        {
            marketingFirm = LoadMarketingFirm();

            CreateMarketingFirmWithManager();

            CreateNewCampaign();  // Loops markertingFirm.CreateSweepstake()
            
            Menu();

        }

        private void Menu()
        {
            string prompt = "Please select an option:\n1) [V]iew Current Sweepstakes\n2) Add [N]ew Sweepstakes\n3) [S]ave Marketing Firm data to file\n4) [E]xit Program";
            string input = UI.GetInputFor(prompt);
            switch (input.ToLower())
            {
                case "v":
                case "view current sweepstakes":
                case "1":
                    marketingFirm.SweepstakesEmployeeMenu();
                    Menu();
                    break;
                case "n":
                case "add new sweepstakes":
                case "2":
                    CreateNewCampaign();
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

        private void CreateNewCampaign()
        {
            do
            {
                marketingFirm.CreateSweepstakes();
            } while (UI.GetInputYesNo("Would you like to add a another Sweepstakes to your campaign?"));
        }

        public void CreateMarketingFirmWithManager()
        {
            if (marketingFirm != null) return;

            ISweepstakeManager sweepstakeManager = CreateSweepstakesManager(UI.GetInputFor("Please Select a Sweepstakes Campaign Type:\n1) Annual\n2) Promotion"));
            marketingFirm = new MarketingFirm(sweepstakeManager); // DI ISweepstakesManager

        }


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

        public void SaveMarketingFirm()
        {
            string fileName = UI.GetInputFor("Please enter a file name (CAUTION NO VALIDATION IMPLEMENTED, USE LEGAL FILE NAMES ONLY)");
            using (Stream stream = File.Open($"{fileName}.bin", FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, marketingFirm);
            }

        }

        public MarketingFirm LoadMarketingFirm()
        {
            if(!UI.GetInputYesNo("Do you have a previous Marketing Firm file you'd like to load?"))
            {
                return null;
            }

            string fileName = UI.GetInputFor("Please enter filename (without *.bin):");
            using (Stream stream = File.Open($"{fileName}.bin", FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (MarketingFirm)binaryFormatter.Deserialize(stream);
            }
        }


    }
}
