using AVPplatformer.Components;
using System.Drawing;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HERO : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private LayerCheck _groundCheck;

    private Rigidbody2D _rigidbody;
    private Vector2 _direction;
    private Animator _animator;
    private SpriteRenderer _sprite;

    private static readonly int IsGroundKey = Animator.StringToHash("isGround");
    private static readonly int IsRunningKey = Animator.StringToHash("isRunning");
    private static readonly int VerticalSpeedKey = Animator.StringToHash("verticalSpeed");


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }
   
    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }
    private void FixedUpdate()
    {
       
        _rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);
        var isJumping = _direction.y > 0;
        if (isJumping)
        {
            if (isGrounded())
            {
                _rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
            }

        }
        else if (_rigidbody.velocity.y > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
        }
        _animator.SetFloat(VerticalSpeedKey, _rigidbody.velocity.y);
        _animator.SetBool(IsGroundKey, _groundCheck.isTouchingLayer);
        _animator.SetBool(IsRunningKey, _direction.x != 0 );


        UpdateSpriteDirection();
            
    }
    private void UpdateSpriteDirection()
    {
        if (_direction.x > 0)
        {
            _sprite.flipX = false;
        }
        else if (_direction.x < 0)
        {
            _sprite.flipX = true;
        }
    }
    public void SaySomething()
    {
        Debug.Log("ОРЕТ");
    }

    private bool isGrounded()
    {
        return _groundCheck.isTouchingLayer;
    }

}
