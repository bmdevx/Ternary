using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Chips
{
    public class LM393 : IMultiIOComponentOld
    {
        //Pin 1
        public event ComponentTriggeredEvent OutputA;
        //Pin 7
        public event ComponentTriggeredEvent OutputB;


        public event ComponentTriggeredEvent ComponentTriggered;
        public ComponentTriggeredEvent[] ComponentTriggeredEvents { get; }


        public Trit InvertingInputAState { get; protected set; }
        public Trit NonInvertingInputAState { get; protected set; }

        public Trit InvertingInputBState { get; protected set; }
        public Trit NonInvertingInputBState { get; protected set; }


        //Pin 2
        protected IComponentOld _InvertingInputA;
        public IComponentOld InvertingInputA
        {
            get { return _InvertingInputA; }
            set
            {
                if (_InvertingInputA != null)
                    _InvertingInputA.ComponentTriggered -= InvertingInputATriggered;

                _InvertingInputA = value;
                if (_InvertingInputA != null)
                    _InvertingInputA.ComponentTriggered += InvertingInputATriggered;
            }
        }

        //Pin 3
        protected IComponentOld _NonInvertingInputA;
        public IComponentOld NonInvertingInputA
        {
            get { return _NonInvertingInputA; }
            set
            {
                if (_NonInvertingInputA != null)
                    _NonInvertingInputA.ComponentTriggered -= NonInvertingInputATriggered;

                _NonInvertingInputA = value;
                if (_NonInvertingInputA != null)
                    _NonInvertingInputA.ComponentTriggered += NonInvertingInputATriggered;
            }
        }


        //Pin 6
        protected IComponentOld _InvertingInputB;
        public IComponentOld InvertingInputB
        {
            get { return _InvertingInputB; }
            set
            {
                if (_InvertingInputB != null)
                    _InvertingInputB.ComponentTriggered -= InvertingInputBTriggered;

                _InvertingInputB = value;
                if (_InvertingInputB != null)
                    _InvertingInputB.ComponentTriggered += InvertingInputBTriggered;
            }
        }

        //Pin 5
        protected IComponentOld _NonInvertingInputB;
        public IComponentOld NonInvertingInputB
        {
            get { return _NonInvertingInputB; }
            set
            {
                if (_NonInvertingInputB != null)
                    _NonInvertingInputB.ComponentTriggered -= NonInvertingInputBTriggered;

                _NonInvertingInputB = value;
                if (_NonInvertingInputB != null)
                    _NonInvertingInputB.ComponentTriggered += NonInvertingInputBTriggered;
            }
        }


        public IEnumerable<IComponentOld> Inputs => throw new NotImplementedException();

        public IEnumerable<IComponentOld> Outputs => throw new NotImplementedException();

        public LM393(Trit invertingInputAState = Trit.Neu, Trit nonInvertingInputAState = Trit.Neu,
            Trit invertingInputBState = Trit.Neu, Trit nonInvertingInputBState = Trit.Neu)
        {
            ComponentTriggeredEvents = new ComponentTriggeredEvent[] { OutputA, OutputB };

            InvertingInputAState = invertingInputAState.Invert();
            NonInvertingInputAState = nonInvertingInputAState;

            InvertingInputBState = invertingInputBState.Invert();
            NonInvertingInputBState = nonInvertingInputBState;
        }


        protected void InvertingInputATriggered(object sender, Trit trit)
        {
            InvertingInputAState = trit.Invert();

            OutputA?.Invoke(this, InvertingInputAState > NonInvertingInputAState ? InvertingInputAState : NonInvertingInputAState);
            ComponentTriggeredEvents[0].Invoke(this, InvertingInputAState > NonInvertingInputAState ? InvertingInputAState : NonInvertingInputAState);
        }

        protected void NonInvertingInputATriggered(object sender, Trit trit)
        {
            NonInvertingInputAState = trit;
            OutputA?.Invoke(this, InvertingInputAState > NonInvertingInputAState ? InvertingInputAState : NonInvertingInputAState);
            ComponentTriggeredEvents[0].Invoke(this, InvertingInputAState > NonInvertingInputAState ? InvertingInputAState : NonInvertingInputAState);
        }


        protected void InvertingInputBTriggered(object sender, Trit trit)
        {
            InvertingInputBState = trit.Invert();
            OutputB?.Invoke(this, InvertingInputAState > NonInvertingInputBState ? InvertingInputAState : NonInvertingInputBState);
            ComponentTriggeredEvents[1]?.Invoke(this, InvertingInputAState > NonInvertingInputBState ? InvertingInputAState : NonInvertingInputBState);
        }

        protected void NonInvertingInputBTriggered(object sender, Trit trit)
        {
            NonInvertingInputBState = trit;
            OutputB?.Invoke(this, InvertingInputAState > NonInvertingInputBState ? InvertingInputAState : NonInvertingInputBState);
            ComponentTriggeredEvents[1]?.Invoke(this, InvertingInputAState > NonInvertingInputBState ? InvertingInputAState : NonInvertingInputBState);
        }
    }
}
