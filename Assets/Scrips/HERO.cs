using AVPplatformer.Components;
using AVPplatformer.Utils;
using AVPplatformer;
using System.Drawing;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;
using UnityEditor.Animations;
using AVPplatformer.Model;

public class HERO : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private float _damagejumpSpeed;
    [SerializeField] private float _slamDownVelocity;
    [SerializeField] private LayerCheck _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _interactionRadius;
    [SerializeField] private LayerMask _interactionLayer;
    [SerializeField] float _gravityChange;
    [SerializeField] private CheckCircleOverlap _attackRange;
    [Space]    [Header("Particles")]

    [SerializeField] private AnimatorController _armed;
    [SerializeField] private AnimatorController _disarmed;

    [SerializeField] private ParticleSystem _hitParticles;
    [SerializeField] private SpawnComponent _footStepsParticle;
    [SerializeField] private SpawnComponent _jumpParticle;
    [SerializeField] private SpawnComponent _fallParticle;
    [SerializeField] private SpawnComponent _attack1Particle;

    private Rigidbody2D _rigidbody;
    private Vector2 _direction;
    private Animator _animator;
    private bool _isGrounded;
    private bool _allowDoubleJump;
    private Collider2D[] _interactionResult = new Collider2D[1];
  //  private int _coins;
    private bool _doubleJumpUsed;
    private bool _isJumping;
    private bool _upWind;
  //  private bool _isArmed;


    private static readonly int IsGroundKey = Animator.StringToHash("isGround");
    private static readonly int IsRunningKey = Animator.StringToHash("isRunning");
    private static readonly int VerticalSpeedKey = Animator.StringToHash("verticalSpeed");
    private static readonly int Hit = Animator.StringToHash("hit");
    private static readonly int AttackKey = Animator.StringToHash("attack");

    private GameSession _session;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

    }

    public  void OnHealthChanged(int currentHealth)
    {
        _session.Data.Hp = currentHealth;
    }


    private void Start()
    {
        _session = FindObjectOfType<GameSession>();

        var health = GetComponent<HealthComponent>();
      
        health.SetHealth(_session.Data.Hp);
        UpdateHeroWeapon();
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
        _rigidbody.velocity = new Vector2(xVelocity, yVelocity);

        _animator.SetFloat(VerticalSpeedKey, _rigidbody.velocity.y);
        _animator.SetBool(IsGroundKey, _isGrounded);
        _animator.SetBool(IsRunningKey, _direction.x != 0);


        UpdateSpriteDirection();

    }
    private float CalculateYVelocity()
    {
        var yVelocity = _rigidbody.velocity.y;
        var isJumpPressing = _direction.y > 0;

        if (_isGrounded)
        {
            _allowDoubleJump = true;
            _isJumping = false;
        }

        if (isJumpPressing)
        {
            _isJumping = true;
            yVelocity = CalculateJumpVelocity(yVelocity);
            if (_isGrounded && _rigidbody.velocity.y <= 0.001f)
            {
                _rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
            }

        }
        else if (_rigidbody.velocity.y > 0 && _isJumping)
        {
            yVelocity *= 0.5f;

        }


        if (_upWind == true )                                                   // Проверка на попадание в поток
        {
            _rigidbody.gravityScale = -1f;                                      // Изменение гравитации героя
        }
        else if (_upWind == false)
        {
            _rigidbody.gravityScale = 2f;
        }

        _upWind = false;
       

        return yVelocity;
       
    }

    private float CalculateJumpVelocity(float yVelocity)
    {
        var isFalling = _rigidbody.velocity.y <= 0.001f;
        if (!isFalling) return yVelocity;

        if (_isGrounded)
        {
            yVelocity += _jumpSpeed;
            _jumpParticle.Spawn();
        }
        else if (_allowDoubleJump)
        {
            yVelocity = _jumpSpeed;
            _allowDoubleJump = false;
            _doubleJumpUsed = true;
            _jumpParticle.Spawn();
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

    public void AddCoins (int _numCoins)
    {
        _session.Data.Coins += _numCoins;
       
        Debug.Log("Всего монет " + _session.Data.Coins);
    }

    private bool isGrounded()
    {
        return _groundCheck.isTouchingLayer;
    }

    public void TakeDamage()
    {
        _isJumping = false;
        _animator.SetTrigger(Hit);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damagejumpSpeed);


        if (_session.Data.Coins > 0)
        {
            SpawnCoins();
        }

    }

    public void SpawnCoins()
    {
        var numCoinsToDispose = Mathf.Min(_session.Data.Coins, 5);
        _session.Data.Coins -= numCoinsToDispose;

        var burst = _hitParticles.emission.GetBurst(0);
        burst.count = numCoinsToDispose;
        _hitParticles.emission.SetBurst(0, burst);

        _hitParticles.gameObject.SetActive(true);
        _hitParticles.Play();
       // Debug.Log("Всего монет " + _session.Data.Coins);
    }
    public void Interact()
    {
        var size = Physics2D.OverlapCircleNonAlloc(transform.position, _interactionRadius, _interactionResult, _interactionLayer);

        for (int i = 0; i < size; i++)
        {
            var interactable = _interactionResult[i].GetComponent<InteractableComponent>();
            interactable?.Interact();
        }
    }
    public void Attack()
    {
        if (!_session.Data.IsArmed) return;
       
        _animator.SetTrigger(AttackKey);

    }

    public void OnAttacking()
    {
        var gos = _attackRange.GetObjectsInRange();

        foreach (var go in gos)
        {
            var hp = go.GetComponent<HealthComponent>();
            if (hp != null && go.CompareTag("ENEMY"))
            {
                hp.ApplyDamage(_damage);
            }
        }
    }

    public void ArmHero()
    {
        _session.Data.IsArmed = true;

        UpdateHeroWeapon();
     }

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.IsInlayer(_groundLayer))
    //    {

    //        var contact = other.contacts;

    //        if (contact.relativeVelocity.y >= _slamDownVelocity)
    //        {
    //            _fallParticle.Spawn();
    //        }
    //    }
    //}
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

        if (_doubleJumpUsed == true)
        {
            _fallParticle.Spawn();
        }
        _doubleJumpUsed = false;

    }
    public void SpawnAttackParticle()
    {
        _attack1Particle.Spawn();
    }

    public void UpWind()
    {
           
        _upWind = true;
    
    }

    private void UpdateHeroWeapon()
    {
        if (_session.Data.IsArmed) 
        {
            _animator.runtimeAnimatorController = _armed;
        }
        else
        {
            _animator.runtimeAnimatorController = _disarmed;
        }
    }
}

