using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _respawnDelay = 2;
    [SerializeField] private int _lives = 3;
    private int _score = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Resume();
        }
        else
        {
            Debug.LogWarning("Game manager already exists");
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        SpawnPlayer();
    }
    private void OnEnable()
    {
        Events.PlayerEvents.onPlayerActionPerformed.Subscribe(HandlePlayerEvents);
        Events.UIEvents.onMenuOpened.Subscribe(Pause);
        Events.UIEvents.onMenuClosed.Subscribe(Resume);
        Events.InGameEvents.onFlagReached.Subscribe(Victory);

    }
    private void OnDisable()
    {
        Events.PlayerEvents.onPlayerActionPerformed.Unsubscribe(HandlePlayerEvents);
        Events.InGameEvents.onCollectiblePickedUp.Unsubscribe(UpdateScore);
        Events.UIEvents.onMenuOpened.Unsubscribe(Pause);
        Events.UIEvents.onMenuClosed.Unsubscribe(Resume);
        Events.InGameEvents.onFlagReached.Unsubscribe(Victory);
    }
    private void HandlePlayerEvents(PlayerAction playerAction)
    {
        switch (playerAction)
        {
            case PlayerAction.Death:
                _lives--;
                if (!GameOver())
                {
                    StartCoroutine(RespawnPlayerCoroutine());

                    Events.UIEvents.onLiveCounterUpdate.Publish(_lives);
                }
                else
                {
                    Events.UIEvents.onGameOver.Publish();
                    Pause();
                }
                break;
        }
    }
    private void Victory()
    {
        Events.UIEvents.onVictory.Publish(_score);
        Pause();
    }
    private void UpdateScore(int value)
    {
        _score += value;
    }
    private bool GameOver()
    {
        return _lives <= 0;
    }
    private void SpawnPlayer()
    {
        _player.gameObject.SetActive(true);
        _player.transform.position = _spawnPoint.position;
        Events.PlayerEvents.onPlayerActionPerformed.Publish(PlayerAction.Spawn);
    }
    private void Pause()
    {
        Time.timeScale = 0f;
    }
    private void Resume()
    {
        Time.timeScale = 1f;
    }
    private IEnumerator RespawnPlayerCoroutine()
    {
        _player.transform.position = _spawnPoint.position;
        _player.gameObject.SetActive(false);
        Events.InGameEvents.onPlayerRespawnStarted.Publish();
        yield return new WaitForSeconds(_respawnDelay);
        Events.InGameEvents.onPlayerRespawnEnded.Publish();
        SpawnPlayer();
    }
}
