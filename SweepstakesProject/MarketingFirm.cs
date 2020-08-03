using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesProject
{
    public class MarketingFirm
    {
        /// <summary>
        /// Marketing Manager using Dependency Injection of the ISweepstakesManager interface.
        /// </summary>
        ISweepstakeManager _manager;
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
            UI.DisplayText($"New Sweepstakes {sweepstakes.Name} has been successfully added!");
        
        }

    }
}
