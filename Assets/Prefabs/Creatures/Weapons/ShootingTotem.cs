using AVPplatformer.Creatures;
using AVPplatformer.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootingTotem : MonoBehaviour
{

    [SerializeField] private List<ShootingTrapWithoutMeleeAI> _objectsToShoot;
    [SerializeField] public Cooldown _cooldown;

    private int _currentIndex = 0; 

    private void Start()
    {
        _currentIndex = 0;
        foreach (var ShootingTrapWithoutMeleeAI in _objectsToShoot)
        {
            ShootingTrapWithoutMeleeAI.enabled = false;
        }
    }

    private void Update()
    {
        var targetInSight = _objectsToShoot.Any(x => x._vision.isTouchingLayer);
        if (targetInSight)
        {
            if (_cooldown.IsReady)
            {
                _objectsToShoot[_currentIndex].RangeAttack();
                _cooldown.Reset();

                _currentIndex++;

                if (_currentIndex >= _objectsToShoot.Count)
                {
                    _currentIndex = 0;
                }
   
            }
        }

    }
}

