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


        /// <summary>
        /// Attaches a component event to listen to the wire
        /// </summary>
        /// <param name="wire">Wire that is being listened to</param>
        /// <param name="input">Component event that will listen to wire</param>
        /// <returns></returns>
        public static Wire operator +(Wire wire, ComponentTriggeredEvent input)
        {
            wire.Output += input;
            return wire;
        }

        /// <summary>
        /// Attaches a wire to a component event
        /// </summary>
        /// <param name="output">Component event that the wire listens to</param>
        /// <param name="wire">Wire that will listen to a component event</param>
        /// <returns></returns>
        public static ComponentTriggeredEvent operator +(ComponentTriggeredEvent output, Wire wire)
        {
            output += wire.Input;
            return output;
        }

        /// <summary>
        /// Attaches a wire to a component
        /// </summary>
        /// <param name="component">Component that the wire listens to</param>
        /// <param name="wire">Wire that will listen to a component</param>
        /// <returns></returns>
        public static IComponent operator +(IComponent component, Wire wire)
        {
            component.Output += wire.Input;
            return component;
        }

        /// <summary>
        /// Attaching one wire to another
        /// </summary>
        /// <param name="outputWire">Wire that will be listened to</param>
        /// <param name="inputWire">Wire that will listen to the other wire</param>
        /// <returns></returns>
        public static Wire operator +(Wire outputWire, Wire inputWire)
        {
            outputWire.Output += inputWire.Input;
            return outputWire;
        }
    }
}
