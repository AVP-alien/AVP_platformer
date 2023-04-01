using AVPplatformer.Utils;
using System;
using UnityEngine;
using UnityEngine.Events;


namespace AVPplatformer.Components
{
    public class EnterTrigger : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private LayerMask _layer = ~0;
        [SerializeField] private EnterEvent _action;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.IsInlayer(_layer)) return;

            if (!string.IsNullOrEmpty(_tag) && !other.gameObject.CompareTag(_tag)) return;
            {
                _action?.Invoke(other.gameObject);

            }
        }

        [Serializable]

        public class EnterEvent : UnityEvent<GameObject>
        {

        }
    }

}



