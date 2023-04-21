using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AVPplatformer.Components

{
    public class CoinsSpawnComponent : MonoBehaviour
    {
        
        [SerializeField] private GameObject coin1Prefab;
        [SerializeField] private GameObject coin2Prefab;
        [SerializeField] private int minGoldCoins = 1;
        [SerializeField] private int maxGoldCoins = 5;
        [SerializeField] private int minSilverCoins = 1;
        [SerializeField] private int maxSilverCoins = 5;

        [System.Obsolete]
        public void CoinsSpawn()
        {
            
                int coin1Count = Random.Range(minGoldCoins, maxGoldCoins + 1);
                int coin2Count = Random.Range(minSilverCoins, maxSilverCoins + 1);

                for (int i = 0; i < coin1Count; i++)
                {
                Instantiate(coin1Prefab, transform.position + new Vector3(Random.RandomRange(0.1f, 1f), Random.RandomRange(0.1f, 2f), 0), Quaternion.identity);
                    
                }

                for (int i = 0; i < coin2Count; i++)
                {
                    Instantiate(coin2Prefab, transform.position + new Vector3(Random.RandomRange(0.1f, 1f),   Random.RandomRange(0.1f, 2f), 0), Quaternion.identity);
                }
            
        }
    }  

}