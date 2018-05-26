using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components
{
    public class Wire : IComponent
    {
        public event ComponentTriggeredEvent Output;

        public void Input(object sender, Trit trit)
        {
            Output?.Invoke(sender, trit);
        }
    }
}
