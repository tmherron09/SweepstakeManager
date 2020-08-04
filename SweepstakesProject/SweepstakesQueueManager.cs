using System;
using System.Collections.Generic;

namespace SweepstakesProject
{
    /// <summary>
    /// Sweepstakes Manager utilizing a Queue Collection as its underlying data source.
    /// </summary>
    [Serializable]
    public class SweepstakesQueueManager : ISweepstakeManager
    {
        /// <summary>
        /// SweepstakesQueueManager underlying Collection storing all Sweepstakes of the Marketing Firm.
        /// </summary>
        Queue<Sweepstakes> queue;

        /// <summary>
        /// Constructs a new Sweepstakes Manager with an underlying data structure of 
        /// </summary>
        public SweepstakesQueueManager()
        {
            queue = new Queue<Sweepstakes>();
        }
        /// <summary>
        /// Inserts a new Sweepstakes at the back of the Queue.
        /// </summary>
        /// <param name="sweepstakes">Sweepstakes to add to campaign.</param>
        public void InsertSweepstakes(Sweepstakes sweepstakes)
        {
            queue.Enqueue(sweepstakes);
        }
        /// <summary>
        /// Returns the current sweepstakes (at front of Queue.) Does not remove from Queue.
        /// </summary>
        /// <returns>Current <see cref="Sweepstakes"/></returns>
        public Sweepstakes GetSweepstakes()
        {
            return queue.Peek();
        }
        /// <summary>
        /// Ends the current sweepstakes and removes from Queue. Calls the Ending Sweepstakes to Pick its Winner.
        /// </summary>
        public void EndSweepstakes()
        {
            Sweepstakes endingSweepstakes = queue.Dequeue();
            string nextSweepstakesName;
            if(queue.Count >0)
            {
                nextSweepstakesName = queue.Peek().Name;
            }
            else
            {
                nextSweepstakesName = "*To be Announced*";
            }

            endingSweepstakes.PickWinner(nextSweepstakesName);
            
            
        }

    }
}
