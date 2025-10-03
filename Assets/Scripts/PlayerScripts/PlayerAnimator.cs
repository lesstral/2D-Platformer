using System;
using TMPro;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public void HandleAnimations(String state, float xDirection)
    {
        switch (state)
        {
            case "Running":
                _animator.SetBool("isRunning", true);
                if (xDirection > 0) _spriteRenderer.flipX = false;
                else if (xDirection < 0) _spriteRenderer.flipX = true;
                break;
            case "Idle":
                Debug.Log("idle");
                _animator.SetBool("isRunning", false);
                break;
        }
    }
}
