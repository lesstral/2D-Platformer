using System;

public class GameEvent
{

    private event Action action = delegate { };
    public void Publish()
    {
        action?.Invoke();
    }
    public void Subscribe(Action subscriber)
    {
        action += subscriber;
    }
    public void Unsubscribe(Action subscriber)
    {
        action -= subscriber;
    }


}
