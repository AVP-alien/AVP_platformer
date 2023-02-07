using AVPplatformer.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AVPplatformer.Components
{
    public class CoinMsgComponent : MonoBehaviour
    {
        public void CoinsMSG()
        {
            var _silver = GetComponent<SilverCoinsCounterComponent>();
            int silver = _silver.SilverCoinsNum;

            var _gold = GetComponent<GoldenCoinsCounterComponent>();
            int gold = _gold.GoldenCoinsNum;

            int Coinsnum = silver + gold;
            Debug.Log("Всего монет " + Coinsnum);


        }
    }
}