using UnityEngine;

public class PlayerSpawnPointVFX : MonoBehaviour
{
    [SerializeField] ParticleSystem _respawnProcessVFX;
    [SerializeField] ParticleSystem _respawnDoneVFX;
    private void OnEnable()
    {
        Events.InGameEvents.onPlayerRespawnStarted.Subscribe(OnPlayerRespawnStarted);
        Events.InGameEvents.onPlayerRespawnEnded.Subscribe(OnPlayerRespawnEnded);
    }
    private void OnDisable()
    {
        Events.InGameEvents.onPlayerRespawnStarted.Unsubscribe(OnPlayerRespawnStarted);
        Events.InGameEvents.onPlayerRespawnEnded.Unsubscribe(OnPlayerRespawnEnded);
    }
    private void OnPlayerRespawnStarted()
    {
        _respawnProcessVFX.Play();
    }
    private void OnPlayerRespawnEnded()
    {
        _respawnProcessVFX.Stop();
        _respawnDoneVFX.Clear();
        _respawnDoneVFX.Play();
    }
}
