﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CheckCircleOverlap : MonoBehaviour
{
    [SerializeField] private float _radius = 1f;
 

    private Collider2D[] _interactionResult = new Collider2D[5];

    public GameObject[] GetObjectsInRange()
    {
        var size = Physics2D.OverlapCircleNonAlloc(
            transform.position, 
            _radius, 
            _interactionResult);

        var overlaps = new List<GameObject>();
        for (var i = 0; i< size; i++)
        {
            overlaps.Add(_interactionResult[i].gameObject);

        }
        return overlaps.ToArray();
    }

    private void OnDrawGizmosSelected()
    {

        // Handles.color = Color TransparentRed = new Color(1f, 0f, 0f,0.1f);

        Handles.DrawSolidDisc(transform.position,Vector3.forward, _radius);
    }
}
