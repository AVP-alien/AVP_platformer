using UnityEngine;

namespace AVPplatformer.Weapons
{
    public class SinusoidalProjectile : BaseProjectile
    {
        [SerializeField] private float _frequency = 1.0f;
        [SerializeField] private float _amplitude = 1.0f;
        private float _originalY;
        private float _time;
        protected override void Start()
        {
            base.Start();
            _originalY = _rigidbody.position.y;
        }

        private void FixedUpdate()
        {
            var position = _rigidbody.position;
            position.x += _direction * _speed;
            position.y = _originalY + _amplitude * Mathf.Sin(_time * _frequency);
            _rigidbody.MovePosition(position);
            _time += Time.fixedDeltaTime;
        }
    }
}