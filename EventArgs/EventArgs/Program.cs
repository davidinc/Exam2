using System;

// Step 1: Create a class to pass as an argument 
public class MyEventArgs : EventArgs
{
    public string Message { get; }

    public MyEventArgs(string message)
    {
        Message = message;
    }
}

public delegate void MyEventHandler(object sender, MyEventArgs args);

public class EventPublisher
{
    public event MyEventHandler MyEvent;

    protected virtual void OnMyEvent(string message)
    {
        MyEventHandler handler = MyEvent;
        handler?.Invoke(this, new MyEventArgs(message));
    }

    public void DoSomething()
    {
        OnMyEvent("Event occurred!");
    }
}

public class EventSubscriber
{
    public EventSubscriber(EventPublisher publisher)
    {

        publisher.MyEvent += HandleEvent;
    }

    private void HandleEvent(object sender, MyEventArgs args)
    {
        Console.WriteLine("Event handled. Message: " + args.Message);
    }
}

class Program
{
    static void Main()
    {
        EventPublisher publisher = new EventPublisher();
        EventSubscriber subscriber = new EventSubscriber(publisher);

       publisher.DoSomething();
    }
}