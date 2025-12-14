using UnityEngine;

public class LevelSoundPlayer : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _respawnProcessClip;
    [SerializeField] AudioClip _victoryClip;

    private void OnEnable()
    {
        Events.InGameEvents.onPlayerRespawnStarted.Subscribe(PlayRespawnClip);
        Events.InGameEvents.onPlayerRespawnEnded.Subscribe(Stop);
    }
    private void OnDisable()
    {
        Events.InGameEvents.onPlayerRespawnStarted.Unsubscribe(PlayRespawnClip);
        Events.InGameEvents.onPlayerRespawnEnded.Unsubscribe(Stop);
    }
    private void PlayRespawnClip()
    {
        _audioSource.clip = _respawnProcessClip;
        _audioSource.Play();
    }
    private void Stop()
    {
        _audioSource.Stop();
    }
}

