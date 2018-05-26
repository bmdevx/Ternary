using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ternary.Components
{
    public abstract class BasicComponentOld : IComponentOld
    {
        public event ComponentTriggeredEvent ComponentTriggered;

        private List<IComponentOld> _Inputs = new List<IComponentOld>();
        public IEnumerable<IComponentOld> Inputs => _Inputs;

        private List<IComponentOld> _Outputs = new List<IComponentOld>();
        public IEnumerable<IComponentOld> Outputs => _Outputs;


        public BasicComponentOld(IEnumerable<IComponentOld> inputs = null, IEnumerable<IComponentOld> outputs = null)
        {
            if (inputs != null)
            {
                foreach (IComponentOld component in inputs)
                    AddInput(component);
            }

            if (outputs != null)
            {
                foreach (IComponentOld component in outputs)
                    AddOutput(component);
            }
        }

        
        public void AddInput(IComponentOld component)
        {
            if (!_Inputs.Contains(component))
            {
                component.ComponentTriggered += Trigger;
                _Inputs.Add(component);
            }
        }

        public void AddInput(IMultiIOComponentOld component, int index)
        {
            //if (!_Inputs.Contains(component))
            //{
                component.ComponentTriggeredEvents[index] += Trigger;
                _Inputs.Add(component);
            //}
        }

        public void RemoveInput(IComponentOld component)
        {
            if (_Inputs.Contains(component))
            {
                component.ComponentTriggered -= Trigger;
                _Inputs.Remove(component);
            }
        }


        public void AddOutput(IComponentOld component)
        {
            if (!_Outputs.Contains(component))
                _Outputs.Add(component);
        }

        public void RemoveOutput(IComponentOld component)
        {
            if (_Outputs.Contains(component))
                _Outputs.Remove(component);
        }


        public void Trigger(object sender, Trit trit)
        {
            ComponentTriggered?.Invoke(this, Execute(sender, trit));
        }

        protected abstract Trit Execute(object sender, Trit trit);
    }
}
