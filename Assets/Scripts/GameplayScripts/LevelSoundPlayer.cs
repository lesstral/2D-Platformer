using UnityEngine;

public class LevelSoundPlayer : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _respawnProcessClip;
    [SerializeField] AudioClip _victoryClip;
    [SerializeField] AudioClip _gameoverClip;


    private void OnEnable()
    {
        Events.InGameEvents.onPlayerRespawnStarted.Subscribe(PlayRespawnClip);
        Events.InGameEvents.onPlayerRespawnEnded.Subscribe(Stop);
        Events.UIEvents.onVictory.Subscribe(PlayVictoryClip);
        Events.UIEvents.onGameOver.Subscribe(PlayGameoverClip);
    }
    private void OnDisable()
    {
        Events.InGameEvents.onPlayerRespawnStarted.Unsubscribe(PlayRespawnClip);
        Events.InGameEvents.onPlayerRespawnEnded.Unsubscribe(Stop);
        Events.UIEvents.onVictory.Unsubscribe(PlayVictoryClip);
        Events.UIEvents.onGameOver.Unsubscribe(PlayGameoverClip);
    }
    private void PlayVictoryClip(int score) => PlayClip(_victoryClip);
    private void PlayGameoverClip() => PlayClip(_gameoverClip);
    private void PlayRespawnClip() => PlayClip(_respawnProcessClip);
    private void PlayClip(AudioClip clip)
    {
        Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }
    private void Stop()
    {
        _audioSource.Stop();
    }
}

