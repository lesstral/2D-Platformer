using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour, IControllable
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] LayerMask _groundLayer;
    private PlayerState _playerState = PlayerState.Idle;
    private PlayerState _previousState = PlayerState.Idle;
    private void OnEnable()
    {
        Events.InGameEvents.onFlagReached.Subscribe(OnVictory);
    }
    private void OnDisable()
    {
        Events.InGameEvents.onFlagReached.Unsubscribe(OnVictory);
    }

    public void Move(Vector2 direction)
    {

        Vector2 velocity = direction * _speed;
        _rigidbody2D.linearVelocity = new Vector2(velocity.x, _rigidbody2D.linearVelocityY);
        HandleStates();
    }
    private void HandleStates()
    {
        _previousState = _playerState;

        if (!IsGrounded())
        {
            _playerState = PlayerState.Airborne;
        }
        else if (_rigidbody2D.linearVelocity.x > 0)
        {
            _playerState = PlayerState.RunningRight;
        }
        else if (_rigidbody2D.linearVelocity.x < 0)
        {
            _playerState = PlayerState.RunningLeft;
        }
        else
        {
            _playerState = PlayerState.Idle;
        }

        HandleStateTransitions();
    }
    private void HandleStateTransitions()
    {
        if (_previousState == PlayerState.Airborne &&
            _playerState != PlayerState.Airborne)
        {
            Events.PlayerEvents.onPlayerActionPerformed
                .Publish(PlayerAction.Land);
        }

        if (_previousState != _playerState)
        {
            Events.PlayerEvents.onPlayerStateChanged
                .Publish(_playerState);
        }
    }

    public void Jump()
    {

        if (IsGrounded())
        {
            _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocityX, _jumpForce);
            Events.PlayerEvents.onPlayerActionPerformed.Publish(PlayerAction.Jump);
        }
    }
    private void OnVictory()
    {
        gameObject.SetActive(false);
    }
    private bool IsGrounded()
    {
#if UNITY_EDITOR
        Debug.DrawRay(_groundCheck.position, transform.forward * 5f, Color.red);
#endif
        return Physics2D.OverlapBox(_groundCheck.position, new Vector2(0.15f, 0.12f), 0f, _groundLayer);

    }

    public void Die()
    {
        Events.PlayerEvents.onPlayerActionPerformed.Publish(PlayerAction.Death);
    }
}
