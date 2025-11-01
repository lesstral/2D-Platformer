using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSoundManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _jumpSound;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        Events.onPlayerActionPerformed.Add(HandleEvent);
    }
    private void OnDisable()
    {
        Events.onPlayerActionPerformed.Remove(HandleEvent);
    }
    private void HandleEvent(PlayerAction playerAction)
    {
        switch (playerAction)
        {
            case PlayerAction.Jump:
                _audioSource.clip = _jumpSound;
                _audioSource.Play();
                break;
            default:

                break;
        }
    }
}
