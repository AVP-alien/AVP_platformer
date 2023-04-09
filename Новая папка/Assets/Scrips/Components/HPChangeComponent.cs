using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AVPplatformer.Components
{
    public class HPChangeComponent : MonoBehaviour
    {
        [SerializeField] private int _HPchange;

        public void ApplyDamage (GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                    healthComponent.ApplyDamage(_HPchange);
                   
            }

        }
        public void ApplyHeal(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ApplyHeal(_HPchange);

            }

        }
    }
}

