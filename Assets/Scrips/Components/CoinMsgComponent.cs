using AVPplatformer.Components;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AVPplatformer.Components
{
    public class CoinMsgComponent : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _hitParticles;
        public int Coinsnum;
        public void CoinsMSG()
        {
            
            var _silver = GetComponent<SilverCoinsCounterComponent>();
            int silver = _silver.SilverCoinsNum;

            var _gold = GetComponent<GoldenCoinsCounterComponent>();
            int gold = _gold.GoldenCoinsNum;

            Coinsnum = silver + gold;
            Debug.Log("Всего монет " + Coinsnum);


        }

        public void SpawnCoins()
        {
            var numCoinsToDispose = Mathf.Min(Coinsnum, 5);
            Coinsnum -= numCoinsToDispose;

            var burst = _hitParticles.emission.GetBurst(0);
            burst.count = numCoinsToDispose;
            _hitParticles.emission.SetBurst(0, burst);

            _hitParticles.gameObject.SetActive(true);
            _hitParticles.Play();
            Debug.Log("Всего монет " + Coinsnum);
        }
    }
}