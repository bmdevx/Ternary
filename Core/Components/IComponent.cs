﻿namespace Ternary.Components
{
    public delegate void ComponentTriggeredEvent(object sender, Trit trit);

    public interface IComponentInput
    {
        void Input(object sender, Trit trit);
    }

    public interface IComponentOutput
    {
        event ComponentTriggeredEvent Output;
    }

    public interface IComponent : IComponentInput, IComponentOutput { }


    public interface IMultiInComponent
    {
        ComponentTriggeredEvent[] Inputs { get; }
    }

    public interface IMultiOutComponent
    {
        ComponentTriggeredEvent[] Outputs { get; }
    }

    public interface IMultiIOComponent : IMultiInComponent, IMultiOutComponent { }
}
