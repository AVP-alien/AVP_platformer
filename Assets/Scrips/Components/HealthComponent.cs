using System;
using UnityEngine;
using UnityEngine.Events;

namespace AVPplatformer.Components

{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] public int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private HealsChangeEvent _onChange;

        public void ApplyDamage(int damageValue)
        {
            if (_health > 0)
            {
                _health -= damageValue;
                _onChange?.Invoke(_health);
                Debug.Log("Осталось здоровья " + _health);
                _onDamage?.Invoke();

            }

            if (_health <= 0)
            {
                _onDie?.Invoke();
            }
        }
        public void ApplyHeal(int HealValue)
        {
            _health += HealValue;
            _onChange?.Invoke(_health);
            Debug.Log("Осталось здоровья " + _health);
        }


        [Serializable]
        public class HealsChangeEvent : UnityEvent<int>
        {

        }

        public void SetHealth(int health)
        {
            _health = health;
        }
    }

}
