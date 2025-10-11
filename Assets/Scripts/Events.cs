using UnityEngine;

public class Events : MonoBehaviour
{
    public static readonly GameEvent<PlayerState> onPlayerStateChanged = new GameEvent<PlayerState>();
}
