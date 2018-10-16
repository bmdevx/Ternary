using System;
using System.Collections.Generic;
using System.Text;
using Ternary.Components.Muxers;

namespace Ternary.Components.Experimental
{
    public class OutIfNotNeuGate : IComponentOutput
    {
        public string ComponentName { get; internal set; }

        public event ComponentTriggeredEvent Output;

        public OutIfNotNeuGate(MatchGate matchGate, IComponentOutput componentOutput)
        {
            Muxer muxer = new Muxer();

            componentOutput.Output += muxer.AInput;
            componentOutput.Output += muxer.CInput;
            matchGate.Output += muxer.InputSelect;

            muxer.Output += (s, t) => Output?.Invoke(this, t);
        }
    }
}
