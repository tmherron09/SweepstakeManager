using System;
using System.Collections.Generic;
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
            bool isRunning = true;

            CreateMarketingFirmWithManager();

            CreateNewCampaign();  // Loops markertingFirm.CreateSweepstake()

            while (isRunning)
            {
                // Ask user if they would like to create a new sweepstakes for their campaign.
                // Or if they would like to view the current sweepstakes.
                // Or Quit the Program.
                // Put switch statement here
                // marketingFirm.CreateSweepstake()
                // marketingFirm.CurrentSweepstakesInfo();

            }
        }

        private void CreateNewCampaign()
        {
            throw new NotImplementedException();
        }

        public void CreateMarketingFirmWithManager()
        {
            // Request Name
            // UI.GetInput();
            // Ask what type of Campaign they would like to start
            // One Time Campaign- SweepstakesStackManager
            // Annual Campaign- SweepstakesQueueManager
            // Set simulation markingFirm to new marketing firm from factory.
            marketingFirm = new MarketingFirm(); // DI ISweepstakesManager

        }


        public ISweepstakeManager CreateSweepstakesManager(string campaign)
        {
            ISweepstakeManager sweepstakeManager;

            switch (campaign)
            {
                case "annual":
                    sweepstakeManager = new SweepstakesStackManager();
                    break;
                case "promotion":
                    sweepstakeManager = new SweepstakesQueueManager();
                    break;
                default:
                    throw new Exception("Campaign type/Sweepstakes Manager type not found");
            }
            return sweepstakeManager;
        }

    }
}
