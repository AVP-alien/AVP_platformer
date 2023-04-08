using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    [SerializeField] public GameObject objectPrefab; // префаб, который вы будете распределять по окружности
    [SerializeField] public int numberOfObjects; // количество объектов на окружности
    [SerializeField] public float radius; // радиус окружности
    [SerializeField] public float speed; // скорость вращения окружности

    private List<GameObject> objectsList = new List<GameObject>();

    void Start()
    {
        float angleStep = 360f / numberOfObjects;

        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * angleStep;
            Vector3 pos = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) ,   Mathf.Sin(Mathf.Deg2Rad * angle) , 0) * radius ;
            Quaternion rot = Quaternion.Euler(0, 0, 0);
            GameObject obj = Instantiate(objectPrefab, pos, rot, transform);
            objectsList.Add(obj);
        }
    }

    void Update()
    {
        float angleStep = 360f / numberOfObjects;
        float angle = 0f;

        Vector3 pos = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) , Mathf.Sin(Mathf.Deg2Rad * angle) , 0) * radius;
        angle += angleStep;

        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
}


