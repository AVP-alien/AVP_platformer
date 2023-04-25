using AVPplatformer.Components;
using AVPplatformer.Components.Aduio;
using AVPplatformer.Utils;
using UnityEngine;

namespace AVPplatformer.Creatures
{
    public class ShootingTrapAI : MonoBehaviour
    {
        [SerializeField] public LayerCheck _vision;

        [Header("Melee")]
        [SerializeField] private CheckCircleOverlap _meleeAttack;
        [SerializeField] private LayerCheck _meleeCanAttack;
        [SerializeField] private Cooldown _meleeCooldown;

        [Header("Range")]
        [SerializeField] private SpawnComponent _rangeAttack;
        [SerializeField] private Cooldown _rangeCooldown;

        private Animator _animator;
        private PlaySoundsComponent _sounds;

        private static readonly int Melee = Animator.StringToHash("melee");
        private static readonly int Range = Animator.StringToHash("range");



        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _sounds = GetComponent<PlaySoundsComponent>();
        }

        private void Update()
        {
            if (_vision.isTouchingLayer)
            {
               
                               
                if (_meleeCanAttack.isTouchingLayer)
                {
                    if (_meleeCooldown.IsReady)
                    {
                        MeleeAttack();
                    }
                    return;
                }
                 if (_rangeCooldown.IsReady)
                {
                    RangeAttack();
                }
            }
        }
        private void MeleeAttack()
        {
            _meleeCooldown.Reset();
            _animator.SetTrigger(Melee);
            _sounds.Play("melee");
        }

        private void RangeAttack()
        {
            _rangeCooldown.Reset();
            _animator.SetTrigger(Range);
        }
        public void OnMeleeAttack()
        {
            _meleeAttack.Check();
        }

        public void OnRangeAttack()
        {
            _rangeAttack.Spawn();
            _sounds.Play("Shoot");
        }
    }

}

