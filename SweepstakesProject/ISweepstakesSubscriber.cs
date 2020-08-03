using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesProject
{
    public interface ISweepstakesSubscriber
    {

        void Notify(string sweepstakesName, Contestant winner);


    }
}
