using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesProject
{
    /// <summary>
    /// Interface defining the Notification method of subscribers to Sweepstakes.
    /// </summary>
    public interface ISweepstakesSubscriber
    {
        /// <summary>
        /// Method called by Sweepstakes. Usually at the end of a sweepstakes to announce a winner.
        /// </summary>
        /// <param name="sweepstakesName">Name of Sweepstakes Notifying subscriber.</param>
        /// <param name="winner">Winning Contestant declared by Sweepstakes.</param>
        void Notify(string sweepstakesName, Contestant winner);


    }
}
