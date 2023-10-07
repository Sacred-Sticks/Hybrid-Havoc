using System;
using Kickstarter.Identification;

public interface IInputReceiver<in T> : IInputReceiver
{
    public void ReceiveInput(T input);
}

public interface IInputReceiver
{
    
}
