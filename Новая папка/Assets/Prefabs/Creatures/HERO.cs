using AVPplatformer.Components;
using UnityEngine;
using UnityEditor.Animations;
using AVPplatformer.Model;
using AVPplatformer.Utils;
using UnityEditor.Experimental.GraphView;

namespace AVPplatformer.Creatures
{
    public class HERO : Creature
    {
        [SerializeField] private CheckCircleOverlap _interactionCheck;
        [SerializeField] private LayerCheck _wallCheck;
        // [SerializeField] private LayerMask _interactionLayer;


        [Header("Params")]
        [SerializeField] private float _slamDownVelocity;
        [SerializeField] private float _interactionRadius;

        [SerializeField] private Cooldown _throwCooldown;
        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;

        [Space]
        [Header("Particles")]
        [SerializeField] private ParticleSystem _hitParticles;

        private static readonly int ThrowKey = Animator.StringToHash("throw");

        private bool _allowDoubleJump;
        private bool _isOnWall;
        //  private int _coins;
        // private bool _doubleJumpUsed;
        private bool _upWind;
        private float _defaultGravityScale;


        private GameSession _session;

        protected override void Awake()
        {
            base.Awake();

            _defaultGravityScale = _rigidbody.gravityScale;
        }



        private void Start()
        {
            _session = FindObjectOfType<GameSession>();

            var health = GetComponent<HealthComponent>();

            health.SetHealth(_session.Data.Hp);
            UpdateHeroWeapon();
        }
        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Hp = currentHealth;
        }

        protected override void Update()
        {
            base.Update();
           //var moveToSameDirection = Direction.x * transform.lossyScale.x > 0;
            //if (_wallCheck.isTouchingLayer && moveToSameDirection)
            //{
            //    _isOnWall = true;
            //    _rigidbody.gravityScale = 0;
            //}
            //else
            //{
            //    _isOnWall = false;
            //    _rigidbody.gravityScale = _defaultGravityScale;
            //}
        }

        protected override float CalculateYVelocity()
        {
            //var yVelocity = _rigidbody.velocity.y;
            var isJumpPressing = _direction.y > 0;

            if (_isGrounded)   // добавить || _isOnWall
            {
                _allowDoubleJump = true;
            }

            //if (!isJumpPressing || _isOnWall)
            //{ return 0f; }


            if (_upWind == true)                                                   // Проверка на попадание в поток
            {
                _rigidbody.gravityScale = -1f;                                      // Изменение гравитации героя
            }
            else if (_upWind == false)
            {
                _rigidbody.gravityScale = 2f;
            }

            _upWind = false;

            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVelocity)
        {
            if (!_isGrounded && _allowDoubleJump)
            {
                //_particles.Spawn("Jump");

                _allowDoubleJump = false;
                //    _doubleJumpUsed = true;

                return _jumpSpeed;
            }
            return base.CalculateJumpVelocity(yVelocity);

        }

        public override void TakeDamage()
        {
            base.TakeDamage();
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
            _interactionCheck.Check();
        }


        public override void Attack()
        {
            if (!_session.Data.IsArmed) return;

            base.Attack();
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
        //            _particles.Spawn("SlamDown");
        //        }
        //    }
        //}



        public void AddCoins(int _numCoins)
        {
            _session.Data.Coins += _numCoins;

            Debug.Log("Всего монет " + _session.Data.Coins);
        }

        public void UpWind()
        {

            _upWind = true;

        }


        public void AddSword(int _numSwords)
        {
            _session.Data.Swords += _numSwords;

            Debug.Log("Всего мечей " + _session.Data.Swords);
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
        public void Throw()
        {

            if (_throwCooldown.IsReady && _session.Data.Swords > 1)
            {
                _animator.SetTrigger(id: ThrowKey);
                _throwCooldown.Reset();
                _session.Data.Swords--;
                Debug.Log("Всего мечей " + _session.Data.Swords);
            }

        }
        //public void ThrowBurst()
        //{
        //    for (int i = 0; i < 3; i++)
        //    {
        //        if ( _session.Data.Swords > 1)
        //        {
        //            _animator.SetTrigger(id: ThrowKey);
        //            _session.Data.Swords--;
        //            Debug.Log("Всего мечей " + _session.Data.Swords);
        //        }
        //    }
        //}

        public void OnDoThrow()
        {
            _particles.Spawn("Throw");


        }
    }
}
