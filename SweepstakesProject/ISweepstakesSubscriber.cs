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
        /// <param name="nextSweepstakesName">Name of Next Sweepstakes in Sweepstakes Manager.</param>
        /// <param name="winner">Winning Contestant declared by Sweepstakes.</param>
        void Notify(string sweepstakesName, string nextSweepstakesName, Contestant winner);


    }
}
