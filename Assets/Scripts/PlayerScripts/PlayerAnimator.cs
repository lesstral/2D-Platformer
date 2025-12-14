using System;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private void OnEnable()
    {
        Events.PlayerEvents.onPlayerStateChanged.Subscribe(HandlePlayerStateEvent);
        Events.PlayerEvents.onPlayerActionPerformed.Subscribe(HandlePlayerActionEvent);
    }
    private void OnDisable()
    {
        Events.PlayerEvents.onPlayerStateChanged.Unsubscribe(HandlePlayerStateEvent);
        Events.PlayerEvents.onPlayerActionPerformed.Unsubscribe(HandlePlayerActionEvent);
    }

    public void HandlePlayerStateEvent(PlayerState playerState)
    {
        switch (playerState)
        {
            case PlayerState.RunningRight:
                _animator.SetBool("isRunning", true);
                _spriteRenderer.flipX = false;

                break;
            case PlayerState.RunningLeft:
                _animator.SetBool("isRunning", true);
                _spriteRenderer.flipX = true;
                break;
            default:
                _animator.SetBool("isRunning", false);
                break;
        }
    }
    public void HandlePlayerActionEvent(PlayerAction playerAction)
    {
        switch (playerAction)
        {
            case PlayerAction.Spawn:

                break;
            case PlayerAction.Death:
                _animator.SetTrigger("Hit");
                break;
            default:
                break;
        }
    }
}
