using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using UnityEngine.Events;
using System.Linq;

public class CheckCircleOverlap : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;
    [SerializeField] private string _tags;
    [SerializeField] private float _radius = 1f;
    [SerializeField] private OnOverlapEvent _onOverlap;

    private Collider2D[] _interactionResult = new Collider2D[10];

    private void OnDrawGizmosSelected()
    {

        // Handles.color = Color TransparentRed = new Color(1f, 0f, 0f,0.1f);

        Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
    }

    public void Check()
    {
        var size = Physics2D.OverlapCircleNonAlloc(
           transform.position,
           _radius,
           _interactionResult,
           _mask);

       
        for (var i = 0; i < size; i++)
        {
            var overlapResult = _interactionResult[i];
            var isInTags = _tags.Any(tags => overlapResult.CompareTag(_tags));
            if (isInTags)
            {
                _onOverlap?.Invoke(_interactionResult[i].gameObject);
            }
           
        }
    }

    [Serializable]
    public class OnOverlapEvent : UnityEvent<GameObject>
    {

    }


}
