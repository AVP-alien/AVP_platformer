using UnityEngine;

namespace AVPplatformer.Components

{
    public class ArmHeroComponent : MonoBehaviour
    {

        public void ArmHero(GameObject go)
        {
            var hero = go.GetComponent<HERO>();
            if (hero != null)
            {
                hero.ArmHero();
            }
        }


    }
}
