using Ternary.Components.Storage;

namespace Ternary.Components.Circuits
{
    public class TrortRegisterCircuit : IBusComponentOutput<Trort>
    {
        public string ComponentName { get; internal set; }

        public event ComponentBusTriggeredEvent<Trort> BusOutput;

        private TrortRegister register;
        private OutIfPosCircuit outIfPosGate;


        public TrortRegisterCircuit(IBusComponentOutput<Trort> dataIn, IComponentOutput rwState,
            IComponentOutput railX, IComponentOutput railY, IComponentOutput railZ, IComponentOutput railT)
        {
            TritMatchCircuit4 addr = new TritMatchCircuit4(Trit.Pos, Trit.Pos, Trit.Pos, Trit.Pos);

            railX.Output += addr.InputA;
            railY.Output += addr.InputB;
            railZ.Output += addr.InputC;
            railT.Output += addr.InputD;

            register = new TrortRegister();

            outIfPosGate = new OutIfPosCircuit(addr, rwState);

            outIfPosGate.Output += register.ReadWriteEnabled;

            dataIn.BusOutput += register.BusInput;

            register.BusOutput += (s, t) => BusOutput?.Invoke(this, t);
        }
    }
}
