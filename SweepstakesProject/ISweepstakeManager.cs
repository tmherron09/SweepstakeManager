using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesProject
{
    public interface ISweepstakeManager
    {
        /// <summary>
        /// Inserts a Sweepstakes into the Sweepstakes Manager class implemeting ISweepstakesManager.
        /// </summary>
        /// <param name="sweepstakes">Sweepstakes to Insert into Collection of Sweepstakes.</param>
        void InsertSweepstakes(Sweepstakes sweepstakes);

        /// <summary>
        /// Method to return the current sweepstakes from a SweepstakesManager.
        /// </summary>
        /// <returns>Current Sweepstakes</returns>
        Sweepstakes GetSweepstakes();
        /// <summary>
        /// Method to end a sweepstakes. Removes Sweepstakes from SweepstakesManager Collection and calls methods needed to complete a sweepstakes.
        /// </summary>
        Contestant EndSweepstakes();

    }
}
