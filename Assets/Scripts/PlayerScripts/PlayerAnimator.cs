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
        Events.PlayerEvents.onPlayerStateChanged.Add(HandlePlayerStateEvent);
        Events.PlayerEvents.onPlayerActionPerformed.Add(HandlePlayerActionEvent);
    }
    private void OnDisable()
    {
        Events.PlayerEvents.onPlayerStateChanged.Remove(HandlePlayerStateEvent);
        Events.PlayerEvents.onPlayerActionPerformed.Remove(HandlePlayerActionEvent);
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
                Debug.Log("idle");
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
