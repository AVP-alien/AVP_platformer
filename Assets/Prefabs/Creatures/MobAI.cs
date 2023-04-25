using AVPplatformer.Components;
using AVPplatformer.Components.Aduio;
using System.Collections;
using UnityEngine;

namespace AVPplatformer.Creatures
{
    public class MobAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private LayerCheck _canAttack;

        [SerializeField] private float _alarmDelay = 0.5f;
        [SerializeField] private float _attackCooldown = 1f;
        [SerializeField] private float _missHeroCooldown = 1f;
        private Coroutine _current;
        private GameObject _target;

        private static readonly int isDeadKey = Animator.StringToHash("is-dead");

        private SpawnListComponent _particles;
        private Creature _creature;
        private Animator _animator;
        public bool _isDead;
        private Patrol _patrol;
        private CapsuleCollider2D _capsuleCollider;
        private PlaySoundsComponent _sounds;


        private void Awake()
        {
            _particles = GetComponent<SpawnListComponent>();
            _creature = GetComponent<Creature>();
            _animator = GetComponent<Animator>();
            _patrol = GetComponent<Patrol>();
            _capsuleCollider = GetComponent<CapsuleCollider2D>();
            _sounds = GetComponent<PlaySoundsComponent>();

        }
        private void Start()
        {
            StartState(_patrol.DoPatrol());
        }

        public void OnHeroInVision(GameObject go)
        {
            if (_isDead) return;
            _target = go;
            StartState(AgroToHero());
        }

        private IEnumerator AgroToHero()
        {
            LookAtHero();
            _particles.Spawn("Exclamation");
            yield return new WaitForSeconds(_alarmDelay);
            StartState(GoToHero());

        }

        private void LookAtHero()
        {
            _creature.SetDirection(Vector2.zero);
            var direction = GetDirectionToTarget();
            _creature.UpdateSpriteDirection(direction);
        }
        private IEnumerator GoToHero()
        {
            while (_vision.isTouchingLayer)
            {
                if (_canAttack.isTouchingLayer)
                {
                    StartState(Attack());
                }
                else
                {
                    SetDirectionToTarget();
                }

                yield return null;

            }
          
            _particles.Spawn("Miss");
            yield return new WaitForSeconds(_missHeroCooldown);

            StartState (_patrol.DoPatrol());

        }

        private IEnumerator Attack()
        {
            while (_canAttack.isTouchingLayer)
            {
                _creature.Attack();
                _sounds.Play("melee");
                yield return new WaitForSeconds(_attackCooldown);
            }
            StartState(GoToHero());
        }

        private void SetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            _creature.SetDirection(direction.normalized);
        }
        private Vector2 GetDirectionToTarget()

        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            return direction.normalized;
        }
        private void StartState(IEnumerator coroutine)
        {
            _creature.SetDirection(Vector2.zero);
            if (_current != null)
            {
                StopCoroutine(_current);
            }
            _current = StartCoroutine(coroutine);

        }

        public void OnDie()
        {
            _isDead = true;
            _animator.SetBool(isDeadKey, true);

            _creature.SetDirection(Vector2.zero);

            _capsuleCollider.direction = CapsuleDirection2D.Horizontal;  // поворот коллайдера
            _capsuleCollider.offset = new Vector2(0f,-0.15f);       // коррекция смащения коллайдера под спрайт
            _capsuleCollider.size = new Vector2(0.7f, 0.5f);        // коррекция размера коллайдера под спрайт


            if (_current != null)

                StopCoroutine(_current);
        }

       

    }


}
