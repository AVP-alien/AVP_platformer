using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


namespace AVPplatformer.Components
{
    public class SilverCoinsCounterComponent : MonoBehaviour
    {

        public int SilverCoinsNum = 0;
      

        public void SilverCoinsCounter ()
        {
            SilverCoinsNum++;
            Debug.Log("Вы подобрали серебряную монетку!" + SilverCoinsNum);
        }
    }
}