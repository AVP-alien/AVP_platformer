using AVPplatformer.Components;
using UnityEngine;

namespace AVPplatformer.Creatures
{
    public class Creature : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private float _speed;
        [SerializeField] protected float _jumpSpeed;
        [SerializeField] private float _damageVelocity;
        [SerializeField] private bool _invertScale;

        [Header("Checkers")]
        [SerializeField] protected LayerMask _groundLayer;
        [SerializeField] private LayerCheck _groundCheck;
        [SerializeField] private CheckCircleOverlap _attackRange;
        [SerializeField] protected SpawnListComponent _particles;

        protected Rigidbody2D _rigidbody;
        protected Vector2 _direction;
        protected Animator _animator;
        protected bool _isGrounded;
        protected bool _isJumping;

        private static readonly int IsGroundKey = Animator.StringToHash("isGround");
        private static readonly int IsRunningKey = Animator.StringToHash("isRunning");
        private static readonly int VerticalSpeedKey = Animator.StringToHash("verticalSpeed");
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int AttackKey = Animator.StringToHash("attack");


        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        protected virtual void Update()
        {
            _isGrounded = _groundCheck.isTouchingLayer;
        }

        private void FixedUpdate()
        {
            var xVelocity = _direction.x * _speed;
            var yVelocity = CalculateYVelocity();
            _rigidbody.velocity = new Vector2(xVelocity, yVelocity);

            _animator.SetFloat(VerticalSpeedKey, _rigidbody.velocity.y);
            _animator.SetBool(IsGroundKey, _isGrounded);
            _animator.SetBool(IsRunningKey, _direction.x != 0);


            UpdateSpriteDirection(_direction);

        }


        protected virtual float CalculateYVelocity()
        {
            var yVelocity = _rigidbody.velocity.y;
            var isJumpPressing = _direction.y > 0;

            if (_isGrounded)
            {
                _isJumping = false;
            }

            if (isJumpPressing)
            {
                _isJumping = true;

                var isFalling = _rigidbody.velocity.y <= 0.001f;
                // if (!isFalling) return yVelocity;

                yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;
            }
            //if (_isGrounded && _rigidbody.velocity.y <= 0.001f)
            //{
            //    _rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
            //}

            else if (_rigidbody.velocity.y > 0 && _isJumping)
            {
                yVelocity *= 0.5f;
            }

            return yVelocity;
        }

        protected virtual float CalculateJumpVelocity(float yVelocity)
        {

            if (_isGrounded)
            {
                yVelocity += _jumpSpeed;
                //  _particles.Spawn( "Jump");
            }


            return yVelocity;

        }

        public void UpdateSpriteDirection(Vector2 direction)
        {
            var multiplier = _invertScale ? -1 : 1;
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(multiplier, 1, 1); ;
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1 * multiplier, 1, 1);
            }
        }
        public virtual void TakeDamage()
        {

            _isJumping = false;
            _animator.SetTrigger(Hit);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damageVelocity);

        }

        public virtual void Attack()
        {
            _animator.SetTrigger(AttackKey);

        }
        public void OnAttacking()
        {
            _attackRange.Check();

        }

    }
}

