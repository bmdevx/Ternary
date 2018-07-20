using System;
using System.Collections.Generic;
using System.Text;
using Ternary.Components.Adders;
using Ternary.Components.Buses.Dyadic;
using Ternary.Components.Buses.Muxers;

namespace Ternary.Components
{
    public class ALU : IBusComponentOutput
    {
        public event ComponentBusTriggeredEvent BusOutput;
        public event ComponentTriggeredEvent OverflowOutput;
        public event ComponentTriggeredEvent SignedOutput;


        private Trit[] _Trits = new Trit[Tryte.NUMBER_OF_TRITS];

        public Tryte BusValue => new Tryte(_Trits);

        /// <summary>
        /// + = Positive Overflow, 0 = No Overflow, - = Negative Overflow
        /// </summary>
        public Trit OverflowState { get; protected set; }
        /// <summary>
        /// + = Positive Number, 0 = Zero, - = Negative Number
        /// </summary>
        public Trit SignedState { get; protected set; }

        /// <summary>
        /// + = A Input is Inverted
        /// </summary>
        public Trit AInversionControlState { get; protected set; }
        /// <summary>
        /// + = B Input is Inverted
        /// </summary>
        public Trit BInversionControlState { get; protected set; }
        /// <summary>
        /// + = A Input is set to 0
        /// </summary>
        public Trit ANegationControlState{ get; protected set; }
        /// <summary>
        /// + = B Input is set to 0
        /// </summary>
        public Trit BNegationControlState { get; protected set; }
        /// <summary>
        /// + = MaxBus, 0 = Addition, - = Minbus
        /// </summary>
        public Trit FowleanControlState { get; protected set; }

        private TryteAdder tryteAdder = new TryteAdder();
        private InverterBus inverterBus = new InverterBus(); //x2
        private MaxBus maxBus = new MaxBus();
        private MinBus minBus = new MinBus();
        
        private Wire _WireAInversionControlInput = new Wire();
        private Wire _WireBInversionControlInput = new Wire();
        private Wire _WireANegationControlInput = new Wire();
        private Wire _WireBNegationControlInput = new Wire();
        private Wire _WireFowleanControlInput = new Wire();

        private MuxerBus muxerBusA = new MuxerBus();
        private MuxerBus muxerBusB = new MuxerBus();


        public ALU()
        {

        }


        public void AInversionInput(object sender, Trit trit)
        {
            AInversionControlState = trit;
            _WireAInversionControlInput.Input(sender, trit);
        }

        public void BInverionInput(object sender, Trit trit)
        {
            BInversionControlState = trit;
            _WireBInversionControlInput.Input(sender, trit);
        }


        public void ANegationInput(object sender, Trit trit)
        {
            ANegationControlState = trit;
            _WireANegationControlInput.Input(sender, trit);
        }

        public void BNegationInput(object sender, Trit trit)
        {
            BNegationControlState = trit;
            _WireBNegationControlInput.Input(sender, trit);
        }


        public void FowleanControlInput(object sender, Trit trit)
        {
            FowleanControlState = trit;
            _WireFowleanControlInput.Input(sender, trit);
        }


        public void ABusInput(object sender, Tryte tryte)
        {
            muxerBusA.AInput(sender, tryte);
        }

        public void BBusInput(object sender, Tryte tryte)
        {
            muxerBusB.BInput(sender, tryte);
        }


        protected void InvokeOutput(object sender, Tryte output)
        {
            BusOutput?.Invoke(this, output);
        }

        protected void InvokeOverflowOutput(object sender, Trit overflow)
        {
            OverflowState = overflow;
            OverflowOutput?.Invoke(this, OverflowState);
        }

        protected void InvokeSignedOutput(object sender, Trit sign)
        {
            SignedState = sign;
            SignedOutput?.Invoke(this, SignedState);
        }
    }
}
