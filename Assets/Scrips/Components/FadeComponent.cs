using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AVPplatformer.Components
{
    public class FadeComponent : MonoBehaviour
    {

        [SerializeField] private float _speed;
        private SpriteRenderer _renderer;


        private void Awake()
        {
            _renderer= GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            var currentAlpha = _renderer.color.a;
            var alpha = Mathf.Lerp(currentAlpha,0f,Time.deltaTime * _speed);
            _renderer.color = new Color(_renderer.color.r, _renderer.color.g,_renderer.color.b, currentAlpha);
        }
    }
}
