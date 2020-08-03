using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SweepstakesProject
{
    [Serializable]
    public class SweepstakesStackManager : ISweepstakeManager
    {
        Stack<Sweepstakes> stack;

        /// <summary>
        /// Constructs a new Sweepstakes Manager with an underlying data structure Stack&lt;Sweepstakes&gt;
        /// </summary>
        public SweepstakesStackManager()
        {
            stack = new Stack<Sweepstakes>();
        }

        /// <summary>
        /// Returns the current sweepstakes (at top of stack.) Does not remove from the Stack.
        /// </summary>
        /// <returns>Current <see cref="Sweepstakes"/></returns>
        public Sweepstakes GetSweepstakes()
        {
            return stack.Peek();
        }
        /// <summary>
        /// Inserts a new SweepStakes to the top of the Stack. It will become the Current Sweepstakes until a new one is inserted or the Sweepstakes has been called to Complete.
        /// </summary>
        /// <param name="sweepstakes">Sweepstakes to add to campaign.</param>
        public void InsertSweepstakes(Sweepstakes sweepstakes)
        {
            stack.Push(sweepstakes);
        }
        /// <summary>
        /// Ends the current Sweepstakes found at the top of the stack and removes it.
        /// </summary>
        public Contestant EndSweepstakes()
        {
            Contestant winner = GetSweepstakes().PickWinner();
            stack.Pop();
            return winner;
        }
    }
}
