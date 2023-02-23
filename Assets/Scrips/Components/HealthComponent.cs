using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AVPplatformer.Components

{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] public int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;

        public void ApplyDamage(int damageValue)
        {
            _health -= damageValue;
            Debug.Log("Осталось здоровья " +  _health);
            _onDamage?.Invoke();
            if (_health <= 0)
            {
                _onDie?.Invoke();
            }
        }
        public void ApplyHeal(int HealValue)
        {
            _health += HealValue;
            Debug.Log("Осталось здоровья " + _health);
        }
    }

}
