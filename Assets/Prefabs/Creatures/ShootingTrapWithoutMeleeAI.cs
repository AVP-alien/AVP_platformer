using AVPplatformer.Components;
using AVPplatformer.Utils;
using System;
using UnityEngine;

namespace AVPplatformer.Creatures
{
    public class ShootingTrapWithoutMeleeAI : MonoBehaviour
    {
        [SerializeField] public LayerCheck _vision;

        [Header("Range")]
        [SerializeField] private SpawnComponent _rangeAttack;
        [SerializeField] private Cooldown _rangeCooldown;

        private Animator _animator;

        private static readonly int Range = Animator.StringToHash("range");



        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Update()
        {
            if (_vision.isTouchingLayer)
            {
          
                 if (_rangeCooldown.IsReady)
                {
                    RangeAttack();
                }
            }
        }


        public void RangeAttack()
        {
            _rangeCooldown.Reset();
            _animator.SetTrigger(Range);
        }

        public void OnRangeAttack()
        {
            _rangeAttack.Spawn();
        }

    }

}

