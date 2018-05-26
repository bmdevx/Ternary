using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components
{
    public interface IComponentOld
    {
        event ComponentTriggeredEvent ComponentTriggered;

        IEnumerable<IComponentOld> Inputs { get; }
        IEnumerable<IComponentOld> Outputs { get; }
    }


    public interface IMultiIOComponentOld : IComponentOld
    {
        ComponentTriggeredEvent[] ComponentTriggeredEvents { get; }
    }
}
