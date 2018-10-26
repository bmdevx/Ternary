using Ternary.Tools;

namespace Ternary.Components.Storage
{
    public class TrortRegister : IBusComponent<Trort>
    {
        public string ComponentName { get; internal set; }

        public event ComponentBusTriggeredEvent<Trort> BusOutput;


        public Trit ReadWriteState { get; private set; }
        
        private Trit[] _Trits = new Trit[Trort.NUMBER_OF_TRITS];

        public Trort Value => new Trort(_Trits);


        private TernaryLatchGate[] _TernaryLatchGates = Create.NewTrortSizedArray(i => new TernaryLatchGate());


        public TrortRegister()
        {
            _TernaryLatchGates[0].Output += (s, t) => { _Trits[0] = t; };
            _TernaryLatchGates[1].Output += (s, t) => { _Trits[1] = t; };
            _TernaryLatchGates[2].Output += (s, t) => { _Trits[2] = t; };
            _TernaryLatchGates[3].Output += (s, t) => { _Trits[3] = t; };
            _TernaryLatchGates[4].Output += (s, t) => { _Trits[4] = t; };
            _TernaryLatchGates[5].Output += (s, t) => { _Trits[5] = t; };
            _TernaryLatchGates[6].Output += (s, t) => { _Trits[6] = t; };
            _TernaryLatchGates[7].Output += (s, t) => { _Trits[7] = t; };
            _TernaryLatchGates[8].Output += (s, t) => { _Trits[8] = t; };
            _TernaryLatchGates[9].Output += (s, t) => { _Trits[9] = t; };
            _TernaryLatchGates[10].Output += (s, t) => { _Trits[10] = t; };
            _TernaryLatchGates[Trort.NUMBER_OF_TRITS - 1].Output += (s, t) =>
            {
                _Trits[Trort.NUMBER_OF_TRITS - 1] = t;
                BusOutput?.Invoke(this, Value);
            };
        }


        /// <summary>
        /// Enables the Read or Write State.
        /// </summary>
        /// <param name="state">+ = Write, - = Read, 0 = Disabled</param>
        public void ReadWriteEnabled(object sender, Trit state)
        {
            ReadWriteState = state;

            for (int i = 0; i < Trort.NUMBER_OF_TRITS; i++)
            {
                _TernaryLatchGates[i].ReadWriteEnabled(this, state);
            }
        }


        public void BusInput(object sender, Trort tryte)
        {
            for (int i = 0; i < Trort.NUMBER_OF_TRITS; i++)
            {
                _TernaryLatchGates[i].Input(this, tryte[i]);
            }
        }
    }
}
