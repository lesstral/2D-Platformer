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
        Events.onPlayerStateChanged.Add(HandleEvent);
    }
    private void OnDisable()
    {
        Events.onPlayerStateChanged.Remove(HandleEvent);
    }

    public void HandleEvent(PlayerState playerState)
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
            case PlayerState.Idle:
                Debug.Log("idle");
                _animator.SetBool("isRunning", false);
                break;
        }
    }
}
