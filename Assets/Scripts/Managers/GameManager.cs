using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _spawnPoint;
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
        Events.PlayerEvents.onPlayerActionPerformed.Add(HandlePlayerEvents);
        Events.UIEvents.onMenuOpened.Add(Pause);
        Events.UIEvents.onMenuClosed.Add(Resume);

    }
    private void OnDisable()
    {
        Events.PlayerEvents.onPlayerActionPerformed.Remove(HandlePlayerEvents);
        Events.GameEvents.onCollectiblePickedUp.Remove(UpdateScore);
        Events.UIEvents.onMenuOpened.Remove(Pause);
        Events.UIEvents.onMenuClosed.Remove(Resume);
    }
    private void HandlePlayerEvents(PlayerAction playerAction)
    {
        switch (playerAction)
        {
            case PlayerAction.Death:
                _lives--;
                if (!GameOver())
                {
                    SpawnPlayer();

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
    }
    private void UpdateScore(int value)
    {

    }
    private bool GameOver()
    {
        return _lives <= 0;
    }
    private void SpawnPlayer()
    {

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

}
