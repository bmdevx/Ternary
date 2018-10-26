using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ternary.Components.Buses;
using Ternary.Components.Gates.Dyadic;
using Ternary.Components.Gates.Monadic;
using Ternary.Components.Muxers;
using Ternary.Reflection;
using Ternary.Tools;

namespace Ternary.Components.Storage
{
    /// <summary>
    /// Faster Simulated Tryte Memory with Trort Number of Addresses
    /// </summary>
    public class STrortAddrTryteMemory : IBusComponent<Tryte>
    {
        private const int TRIBBLE_SIZE = 27;

        public string ComponentName { get; internal set; }
        public event ComponentBusTriggeredEvent<Tryte> BusOutput;

        public Trit ReadWriteState { get; private set; }
        public Trort Address { get; private set; }

        private TryteRegister[,,,] _Registers = new TryteRegister[TRIBBLE_SIZE, TRIBBLE_SIZE, TRIBBLE_SIZE, TRIBBLE_SIZE];
        
        public TryteRegister CurrentRegister { get; private set; }

        private DataBus<Tryte> _DataBus = new DataBus<Tryte>();

        public STrortAddrTryteMemory()
        {
            for (int x = 0; x < TRIBBLE_SIZE; x++)
            {
                for (int y = 0; y < TRIBBLE_SIZE; y++)
                {
                    for (int z = 0; z < TRIBBLE_SIZE; z++)
                    {
                        for (int t = 0; t < TRIBBLE_SIZE; t++)
                        {
                            TryteRegister register = new TryteRegister();

                            _DataBus.BusOutput += register.BusInput;

                            register.BusOutput += (s, val) => BusOutput?.Invoke(this, val);

                            _Registers[x, y, z, t] = register;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Enables the Read or Write State.
        /// </summary>
        /// <param name="state">+ = Write, - = Read, 0 = Disabled</param>
        public void ReadWriteEnabled(object sender, Trit state)
        {
            ReadWriteState = state;

            CurrentRegister?.ReadWriteEnabled(this, state);
        }

        /// <summary>
        /// Sets the Address that will be written / read from
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="addr">Address to select</param>
        public void AddressInput(object sender, Trort addr)
        {
            Address = new Trort(addr);

            CurrentRegister?.ReadWriteEnabled(this, Trit.Neu);

            CurrentRegister = _Registers[
                Address.Tribble0Value + 13,
                Address.Tribble1Value + 13,
                Address.Tribble2Value + 13,
                Address.Tribble3Value + 13];

            CurrentRegister.ReadWriteEnabled(this, ReadWriteState);
        }

        /// <summary>
        /// Sets the Value that will be written to a Register
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tryte">Value to be written</param>
        public void BusInput(object sender, Tryte tryte)
        {
            _DataBus.BusInput(this, tryte);
        }
    }
}
