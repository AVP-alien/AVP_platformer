using System.Collections;
using UnityEngine;

namespace AVPplatformer.Creatures
{
    public abstract class Patrol : MonoBehaviour
    {
        public abstract IEnumerator DoPatrol();
       
    }

}

