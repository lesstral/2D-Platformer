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

    public void Move(Vector2 direction)
    {

        Vector2 velocity = direction * _speed;
        _rigidbody2D.linearVelocity = new Vector2(velocity.x, _rigidbody2D.linearVelocityY);
        HandleStates();
    }
    public void HandleStates()
    {
        if (_rigidbody2D.linearVelocity.x > 0)
        {
            _playerState = PlayerState.RunningRight;
        }
        else if (_rigidbody2D.linearVelocity.x < 0)
        {
            _playerState = PlayerState.RunningLeft;
        }
        else if (_rigidbody2D.linearVelocity.x == 0 || !IsGrounded())
        {
            _playerState = PlayerState.Idle;
        }
        Events.PlayerEvents.onPlayerStateChanged.Publish(_playerState);
    }
    public void Jump()
    {
        Debug.Log("Jump" + IsGrounded());
        if (IsGrounded())
        {
            _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocityX, _jumpForce);
            Events.PlayerEvents.onPlayerActionPerformed.Publish(PlayerAction.Jump);
        }
    }
    private bool IsGrounded()
    {
        Debug.DrawRay(_groundCheck.position, transform.forward * 5f, Color.red);

        return Physics2D.OverlapBox(_groundCheck.position, new Vector2(0.15f, 0.1f), 0f, _groundLayer);

    }
}
