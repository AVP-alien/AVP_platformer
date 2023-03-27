using UnityEngine;


namespace AVPplatformer.Components
{
    public class SilverCoinsCounterComponent : MonoBehaviour
    {
        [SerializeField] private int _numCoins;
        private Creatures.HERO _hero;

        private void Start()
        {
            _hero = FindObjectOfType<Creatures.HERO>();
        }

        public void Add()
        {
            _hero.AddCoins(_numCoins);
            Debug.Log("Вы подобрали серебряную монетку!");
        }
    }
}