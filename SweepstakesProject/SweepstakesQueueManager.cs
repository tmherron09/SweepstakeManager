using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesProject
{
    public class SweepstakesQueueManager : ISweepstakeManager
    {

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
        /// Ends the current sweepstakes and removes from Queue. Returns winner of the Sweepstakes.
        /// </summary>
        public Contestant EndSweepstakes()
        {
            Contestant winner = GetSweepstakes().PickWinner();
            queue.Dequeue();
            return winner;
        }

    }
}
