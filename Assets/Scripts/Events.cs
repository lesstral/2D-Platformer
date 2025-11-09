using UnityEngine;

public class Events : MonoBehaviour
{
    public class PlayerEvents
    {
        public static readonly GameEvent<PlayerState> onPlayerStateChanged = new GameEvent<PlayerState>();
        public static readonly GameEvent<PlayerAction> onPlayerActionPerformed = new GameEvent<PlayerAction>();
        public static readonly GameEvent<int> onCollectiblePickedUp = new GameEvent<int>();
    }

    // UI EVENTS
    public class UI
    {
        public static readonly GameEvent<int> onScoreUpdate = new GameEvent<int>();
    }

}
