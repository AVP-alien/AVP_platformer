using AVPplatformer.Creatures;
using AVPplatformer.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootingTotem : MonoBehaviour
{
    [SerializeField] public List<LayerCheck> _vision;
    [SerializeField] private List<GameObject> _objectsToShoot;// список стреляющих объектов
  
   private int _currentIndex = 0; // индекс текущего объекта

    [SerializeField] private Cooldown _cooldown;
    private int currentIndex = 0;

    private void Start()
    {
        currentIndex = 0;
    }

    private void Update()
    {
        if (_vision.Any(x => x.isTouchingLayer) )
        {
            if (_cooldown.IsReady)
            {
                // получаем текущий объект из списка
                GameObject currentObject = _objectsToShoot[currentIndex];

                // выполняем выстрел с текущего объекта
                currentObject.GetComponent<ShootingTrapWithoutMeleeAI>().RangeAttack();

                // увеличиваем индекс текущего объекта
                currentIndex++;

                // если индекс достиг конца списка, возвращаем его к началу
                if (currentIndex >= _objectsToShoot.Count)
                {
                    currentIndex = 0;
                }
            }
        }

    }
}

