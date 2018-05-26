using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components
{
    public delegate void ComponentTriggeredEvent(object sender, Trit trit);

    public interface IComponent
    {
        event ComponentTriggeredEvent Output;
    }

    public interface IMultiIOComponent
    {
        ComponentTriggeredEvent[] Outputs { get; }
        ComponentTriggeredEvent[] Inputs { get; }
    }
}
