using System;
using System.Collections.Generic;
using System.Text;
using Ternary.Components.Adders;
using Ternary.Components.Buses.Dyadic;
using Ternary.Components.Buses.Monadic;
using Ternary.Components.Buses.Muxers;
using Ternary.Components.Gates.Dyadic;
using Ternary.Components.Gates.Monadic;

namespace Ternary.Components
{
    //Notes: The negation and inversion signals should always be -1 or 1, never 0


    public class ALU : IBusComponentOutput
    {
        public event ComponentBusTriggeredEvent BusOutput;
        public event ComponentTriggeredEvent OverflowOutput;
        public event ComponentTriggeredEvent SignedOutput;

        public Tryte BusValue { get; private set; }

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
        public Trit ANegationControlState { get; protected set; }
        /// <summary>
        /// + = B Input is set to 0
        /// </summary>
        public Trit BNegationControlState { get; protected set; }
        /// <summary>
        /// + = MaxBus, 0 = Addition, - = Minbus
        /// </summary>
        public Trit FowleanControlState { get; protected set; }

        private TryteAdder tryteAdder = new TryteAdder();

        private MaxBus maxBus = new MaxBus();
        private MinBus minBus = new MinBus();
        
        private MuxerBus muxerBusA = new MuxerBus();
        private MuxerBus muxerBusB = new MuxerBus();
        private MuxerBus muxerBus2 = new MuxerBus();
        private MuxerBus muxerBusF = new MuxerBus();

        private InverterBus inverterBusA = new InverterBus();
        private InverterBus inverterBusB = new InverterBus();
        private InverterBus inverterBusOutput = new InverterBus();

        private MaxGate maxGateA = new MaxGate(inputStateA: Trit.Neg, inputStateB: Trit.Neg);
        private MaxGate maxGateB = new MaxGate(inputStateA: Trit.Neg, inputStateB: Trit.Neg);

        private ShiftDownGate shiftDownGateA = new ShiftDownGate(inputState: Trit.Neg);
        private ShiftDownGate shiftDownGateB = new ShiftDownGate(inputState: Trit.Neg);



        public ALU()
        {
            inverterBusA.BusOutput += muxerBusA.BInput;
            inverterBusB.BusOutput += muxerBusB.BInput;

            maxGateA.Output += muxerBusA.InputSelect;
            maxGateB.Output += muxerBusB.InputSelect;

            shiftDownGateA.Output += maxGateA.BInput;
            shiftDownGateB.Output += maxGateA.BInput;

            muxerBusA.BusOutput += tryteAdder.ABusInput;
            muxerBusA.BusOutput += maxBus.ABusInput;
            muxerBusA.BusOutput += minBus.ABusInput;
            
            muxerBusB.BusOutput += tryteAdder.BBusInput;
            muxerBusB.BusOutput += maxBus.BBusInput;
            muxerBusB.BusOutput += minBus.BBusInput;

            tryteAdder.CarryOut += InvokeOverflowOutput;

            minBus.BusOutput += muxerBus2.AInput;
            tryteAdder.BusOutput += muxerBus2.BInput;
            maxBus.BusOutput += muxerBus2.CInput;

            muxerBus2.BusOutput += muxerBusF.AInput;
            muxerBus2.BusOutput += muxerBusF.BInput;
            muxerBus2.BusOutput += inverterBusOutput.BusInput;

            inverterBusOutput.BusOutput += muxerBusF.CInput;

            muxerBusF.BusOutput += InvokeOutput;
        }


        public void AInversionInput(object sender, Trit trit)
        {
            AInversionControlState = trit;
            shiftDownGateA.Input(sender, trit);
        }

        public void BInversionInput(object sender, Trit trit)
        {
            BInversionControlState = trit;
            shiftDownGateB.Input(sender, trit);
        }


        public void ANegationInput(object sender, Trit trit)
        {
            ANegationControlState = trit;
            maxGateA.AInput(sender, trit);
        }

        public void BNegationInput(object sender, Trit trit)
        {
            BNegationControlState = trit;
            maxGateB.AInput(sender, trit);
        }


        public void FowleanControlInput(object sender, Trit trit)
        {
            FowleanControlState = trit;
            muxerBus2.InputSelect(sender, trit);
        }


        public void ABusInput(object sender, Tryte tryte)
        {
            muxerBusA.AInput(sender, tryte);
            inverterBusA.BusInput(sender, tryte);
        }

        public void BBusInput(object sender, Tryte tryte)
        {
            muxerBusB.AInput(sender, tryte);
            inverterBusB.BusInput(sender, tryte);
        }


        protected void InvokeOutput(object sender, Tryte output)
        {
            BusValue = output;
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
