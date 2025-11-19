using System;

public class GameEvent
{

    private event Action action = delegate { };
    public void Publish()
    {
        action?.Invoke();
    }
    public void Add(Action subscriber)
    {
        action += subscriber;
    }
    public void Remove(Action subscriber)
    {
        action -= subscriber;
    }


}
