using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AVPplatformer.Components
{
    public class GoldenCoinsCounterComponent : MonoBehaviour
    {

        public int GoldenCoinsNum = 0;

        public void GoldenCoinsCounter()
        {
            GoldenCoinsNum += 10;

            Debug.Log("Вы подобрали золотую монетку!");


        }
    }
}