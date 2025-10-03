using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IControllable
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] PlayerAnimator _playerAnimator;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Move(Vector2 direction)
    {

        Vector2 velocity = direction * _speed;
        _rigidbody2D.linearVelocity = new Vector2(velocity.x, _rigidbody2D.linearVelocityY);
        if (_rigidbody2D.linearVelocity.x != 0) _playerAnimator.HandleAnimations("Running", _rigidbody2D.linearVelocity.x);
        else if (!IsGrounded() || _rigidbody2D.linearVelocity.y != 0) _playerAnimator.HandleAnimations("Idle", _rigidbody2D.linearVelocity.x);
        else { _playerAnimator.HandleAnimations("Idle", _rigidbody2D.linearVelocity.x); }

    }
    public void Jump()
    {
        Debug.Log("Jump" + IsGrounded());
        if (IsGrounded()) _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocityX, _jumpForce);
    }
    private bool IsGrounded()
    {
        Debug.DrawRay(_groundCheck.position, transform.forward * 5f, Color.red);

        return Physics2D.OverlapBox(_groundCheck.position, new Vector2(0.18f, 0.1f), 0f, _groundLayer);
    }
}
