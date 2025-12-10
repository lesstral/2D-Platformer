using System;

public class GameEvent<T>
{

    private event Action<T> action;
    public void Publish(T param)
    {
        action?.Invoke(param);
    }
    public void Subscribe(Action<T> subscriber)
    {
        action += subscriber;
    }
    public void Unsubscribe(Action<T> subscriber)
    {
        action -= subscriber;
    }



}
