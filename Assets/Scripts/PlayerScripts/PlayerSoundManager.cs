using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _actionAudioSource;
    [SerializeField] private AudioSource _stateAudioSource;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _pickupSound;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _spawnSound;
    [SerializeField] private AudioClip _landSound;
    [SerializeField] private AudioClip _runningSound;
    private void Start()
    {
        _stateAudioSource.clip = _runningSound;
    }
    private void OnEnable()
    {
        Events.PlayerEvents.onPlayerActionPerformed.Subscribe(HandleActionEvent);
        Events.PlayerEvents.onPlayerStateChanged.Subscribe(HandleStateEvent);
    }
    private void OnDisable()
    {
        Events.PlayerEvents.onPlayerActionPerformed.Unsubscribe(HandleActionEvent);
        Events.PlayerEvents.onPlayerStateChanged.Unsubscribe(HandleStateEvent);
    }
    private void HandleActionEvent(PlayerAction playerAction)
    {
        switch (playerAction)
        {
            case PlayerAction.Jump:
                _actionAudioSource.PlayOneShot(_jumpSound);
                break;
            case PlayerAction.PickUp:
                _actionAudioSource.PlayOneShot(_pickupSound);
                break;
            case PlayerAction.Hit:
                _actionAudioSource.PlayOneShot(_hitSound);
                break;
            case PlayerAction.Spawn:
                _actionAudioSource.PlayOneShot(_spawnSound);
                break;
            case PlayerAction.Death:
                _actionAudioSource.PlayOneShot(_deathSound);
                break;
            case PlayerAction.Land:
                _actionAudioSource.PlayOneShot(_landSound);
                break;
            default:
                break;
        }
    }
    private void HandleStateEvent(PlayerState playerState)
    {
        if (playerState == PlayerState.RunningRight
        || playerState == PlayerState.RunningLeft)
        {
            if (!_stateAudioSource.isPlaying)
            {
                _stateAudioSource.Play();
            }
        }
        else
        {
            if (_stateAudioSource.isPlaying)
            {
                _stateAudioSource.Stop();
            }
        }
    }

}
