using UnityEngine;


namespace AVPplatformer.Components
{
    public class VerticalLevitationComponent : MonoBehaviour
    {
        [SerializeField] private float _frequency = 1.0f;
        [SerializeField] private float _amplitude = 1.0f;
        [SerializeField] private bool _randomize;

        private float _originalY;
        private Rigidbody2D _rigidbody;
        private float _seed;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _originalY = _rigidbody.position.y;
            if (_randomize )
            {
                _seed = Random.value * 2 *Mathf.PI;
            }
        }

        private void Update()
        {
            var pos = _rigidbody.position;
            pos.y = _originalY + _amplitude * Mathf.Sin(_seed + Time.time * _frequency);
            _rigidbody.MovePosition(pos);
        }
    }
}

