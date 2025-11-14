using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSoundManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _pickupSound;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _spawnSound;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        Events.PlayerEvents.onPlayerActionPerformed.Add(HandleEvent);
    }
    private void OnDisable()
    {
        Events.PlayerEvents.onPlayerActionPerformed.Remove(HandleEvent);
    }
    private void HandleEvent(PlayerAction playerAction)
    {
        switch (playerAction)
        {
            case PlayerAction.Jump:
                _audioSource.PlayOneShot(_jumpSound);
                break;
            case PlayerAction.PickUp:
                _audioSource.PlayOneShot(_pickupSound);
                break;
            case PlayerAction.Hit:
                _audioSource.PlayOneShot(_hitSound);
                break;
            case PlayerAction.Spawn:
                _audioSource.PlayOneShot(_spawnSound);
                break;
            case PlayerAction.Death:
                _audioSource.PlayOneShot(_deathSound);
                break;
            default:
                break;
        }
    }
}
