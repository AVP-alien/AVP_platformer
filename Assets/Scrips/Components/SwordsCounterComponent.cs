using UnityEngine;


namespace AVPplatformer.Components
{
    public class SwordsCounterComponent : MonoBehaviour
    {
        [SerializeField] private int _numSwords;
        private Creatures.HERO _hero;

        private void Start()
        {
            _hero = FindObjectOfType<Creatures.HERO>();
        }

        public void Add()
        {
            _hero.AddSword(_numSwords);
            Debug.Log("Вы подобрали меч!");
         
        }
    }
}