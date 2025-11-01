using UnityEngine;

public class Events : MonoBehaviour
{
    public static readonly GameEvent<PlayerState> onPlayerStateChanged = new GameEvent<PlayerState>();
    public static readonly GameEvent<PlayerAction> onPlayerActionPerformed = new GameEvent<PlayerAction>();
}
