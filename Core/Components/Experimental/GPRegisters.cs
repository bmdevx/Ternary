using System;
using System.Collections.Generic;
using System.Text;
using Ternary.Components.Buses;
using Ternary.Components.Storage;
using Ternary.Tools;

namespace Ternary.Components.Experimental
{
    public class GPRegisters : IBusComponentOutput<Trort>
    {
        private const int TRIBBLE_SIZE = 27;

        public string ComponentName { get; internal set; }

        public event ComponentBusTriggeredEvent<Trort> BusOutput;


        private TryteRegister[] _TryteRegisters = new TryteRegister[TRIBBLE_SIZE];
        private TrortRegister[] _TrortRegisters = new TrortRegister[TRIBBLE_SIZE];

        private DataBus<Tryte> _TryteDataWire = new DataBus<Tryte>();
        private DataBus<Trort> _TrortDataWire = new DataBus<Trort>();

        private TryteRegister tryteRegister;
        private TrortRegister trortRegister;

        private Trit rwEnabledState;


        public GPRegisters()
        {
            for (int i = 0; i < TRIBBLE_SIZE; i++)
            {
                TryteRegister treg = new TryteRegister();
                _TryteDataWire.BusOutput += treg.BusInput;
                treg.BusOutput += (s, t) => BusOutput?.Invoke(this, new Trort(t));
                _TryteRegisters[i] = treg;

                TrortRegister treg2 = new TrortRegister();
                _TrortDataWire.BusOutput += treg2.BusInput;
                treg2.BusOutput += (s, t) => BusOutput?.Invoke(this, t);
                _TrortRegisters[i] = treg2;
            }
        }


        public void Address(object sender, Tryte address)
        {
            int addr = address.ToInt();
            if (addr < 0)
            {
                throw new IndexOutOfRangeException();
            }
            else if (addr < TRIBBLE_SIZE)
            {
                trortRegister = null;
                tryteRegister.ReadWriteEnabled(this, Trit.Neu);
                tryteRegister = _TryteRegisters[addr];
                tryteRegister.ReadWriteEnabled(this, rwEnabledState);
            }
            else if (addr < TRIBBLE_SIZE * 2)
            {
                tryteRegister = null;
                trortRegister.ReadWriteEnabled(this, Trit.Neu);
                trortRegister = _TrortRegisters[addr];
                trortRegister.ReadWriteEnabled(this, rwEnabledState);
            }
        }
        
        public void DataInput(object sender, IEnumerable<Trit> trits)
        {
            _TryteDataWire.BusInput(this, trits.ToTryte());
            _TrortDataWire.BusInput(this, trits.ToTrort());
        }

        public void ReadWriteEnabled(object sender, Trit rwEnabled)
        {
            rwEnabledState = rwEnabled;

            if (tryteRegister != null)
                tryteRegister.ReadWriteEnabled(this, rwEnabledState);
            else if (trortRegister != null)
                trortRegister.ReadWriteEnabled(this, rwEnabledState);
        }
    }
}
