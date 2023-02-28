using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AVPplatformer.Components
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _target;

        [ContextMenu("Spawn")]
        public void Spawn ()
        {
           var instantiate =  Instantiate(_prefab, _target.position, Quaternion.identity);
            instantiate.transform.localScale = _target.lossyScale;



        }

    }
}
