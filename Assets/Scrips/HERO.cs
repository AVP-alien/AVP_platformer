using AVPplatformer.Components;
using System.Drawing;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HERO : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _damagejumpSpeed;
    [SerializeField] private LayerCheck _groundCheck;
    [SerializeField] private float _interactionRadius;
    [SerializeField] private LayerMask _interactionLayer;

    [SerializeField] private SpawnComponent _footStepsParticle;
    [SerializeField] private SpawnComponent _jumpParticle;
    [SerializeField] private SpawnComponent _fallParticle;

    
    private Rigidbody2D _rigidbody;
    private Vector2 _direction;
    private Animator _animator;
    private bool _isGrounded;
    private bool _allowDoubleJump;
    private Collider2D[] _interactionResult = new Collider2D[1];
    int _coins;
    bool _doubleJumpUsed;
  

    private static readonly int IsGroundKey = Animator.StringToHash("isGround");
    private static readonly int IsRunningKey = Animator.StringToHash("isRunning");
    private static readonly int VerticalSpeedKey = Animator.StringToHash("verticalSpeed");
     private static readonly int Hit = Animator.StringToHash("hit");

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    
    }
   
    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    private void Update()
    {
        _isGrounded = isGrounded();
    }
    private void FixedUpdate()
    {
        var xVelocity = _direction.x * _speed;
        var yVelocity = CalculateYVelocity();
        _rigidbody.velocity= new Vector2(xVelocity, yVelocity);

        _animator.SetFloat(VerticalSpeedKey, _rigidbody.velocity.y);
        _animator.SetBool(IsGroundKey, _isGrounded);
        _animator.SetBool(IsRunningKey, _direction.x != 0 );


        UpdateSpriteDirection();
            
    }
    private float CalculateYVelocity()
    {
        var yVelocity = _rigidbody.velocity.y;
        var isJumpPressing = _direction.y > 0;
        
        if (_isGrounded) _allowDoubleJump= true;

        if (isJumpPressing)
        {
            yVelocity = CalculateJumpVelocity (yVelocity);
            if (_isGrounded && _rigidbody.velocity.y <= 0.001f)
            {
                _rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
            }

        }
        else if (_rigidbody.velocity.y > 0)
        {
            yVelocity *= 0.5f;
            
        }
        return yVelocity;
    }

    private float CalculateJumpVelocity(float yVelocity)
    {
        var isFalling = _rigidbody.velocity.y <= 0.001f;
        if (!isFalling) return yVelocity;

        if (_isGrounded)
        {
            yVelocity += _jumpSpeed;
         }
        else if (_allowDoubleJump)
        {
            yVelocity = _jumpSpeed;
            _allowDoubleJump= false;
            _doubleJumpUsed = true;
        }
        
           
        return yVelocity;
       
    }
    private void UpdateSpriteDirection()
    {
        if (_direction.x > 0)
        {
            transform.localScale = Vector3.one;
             }
        else if (_direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
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

    public void TakeDamage()
    {
        _animator.SetTrigger(Hit);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damagejumpSpeed);

        var coins = GetComponent<CoinMsgComponent>();
        _coins = coins.Coinsnum;

        if (_coins > 0)
        {
            coins.SpawnCoins();
        }

    }

   
    public void Interact()
    {
        var size = Physics2D.OverlapCircleNonAlloc(transform.position, _interactionRadius, _interactionResult, _interactionLayer);

        for (int i= 0; i < size; i++)
        {
            var interactable = _interactionResult[i].GetComponent<InteractableComponent>();
            interactable?.Interact();
        }
    }

    public void SpawnFootDust()
    {
        _footStepsParticle.Spawn();
    }

    public void SpawnJumpDust()
    {
        _jumpParticle.Spawn();
    }

    public void SpawnFallDust()
    {
        //
        //Debug.Log("дабл джамп" + _doubleJumpUsed );
        if (_doubleJumpUsed == true)
        {
            _fallParticle.Spawn();
        }
        _doubleJumpUsed = false;
      
    }
}

