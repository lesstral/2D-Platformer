using UnityEngine;

public class Events
{
    public class PlayerEvents
    {
        public static readonly GameEvent<PlayerState> onPlayerStateChanged = new GameEvent<PlayerState>();
        public static readonly GameEvent<PlayerAction> onPlayerActionPerformed = new GameEvent<PlayerAction>();

    }
    public class InGameEvents
    {
        public static readonly GameEvent<int> onCollectiblePickedUp = new GameEvent<int>();
        public static readonly GameEvent onFlagReached = new GameEvent();
        public static readonly GameEvent onPlayerRespawnStarted = new GameEvent();
        public static readonly GameEvent onPlayerRespawnEnded = new GameEvent();
    }
    public class UIEvents
    {
        public static readonly GameEvent<int> onScoreUpdate = new GameEvent<int>();
        public static readonly GameEvent<int> onLiveCounterUpdate = new GameEvent<int>();
        public static readonly GameEvent onMenuOpened = new GameEvent();
        public static readonly GameEvent onMenuClosed = new GameEvent();
        public static readonly GameEvent menuOnKeyOpen = new GameEvent();
        public static readonly GameEvent onGameOver = new GameEvent();
        public static readonly GameEvent<int> onVictory = new GameEvent<int>();
        public static readonly GameEvent onLevelsLoaded = new GameEvent();
        public static readonly GameEvent onResolutionChange = new GameEvent();
    }

}
