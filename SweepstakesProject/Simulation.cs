﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesProject
{
    public class Simulation
    {
        MarketingFirm marketingFirm;
        Sweepstakes sweepstakes;

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

            CreateMarketingFirmWithManager();

            CreateNewCampaign();  // Loops markertingFirm.CreateSweepstake()

            Menu();

        }

        private void Menu()
        {
            string prompt = "Please select an option:\n1) [V]iew Current Sweepstakes\n2) Add [N]ew Sweepstakes\n3) [E]xit Program";
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
                case "e":
                case "exit program":
                case "3":
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

    }
}
