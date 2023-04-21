using System.Collections;
using UnityEngine;


namespace AVPplatformer.Creatures
{
    public class PlatformPatrol : Patrol
    {

        [SerializeField] private float _rayDistance;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private Vector2 _direction = Vector2.left;

        private Creature _creature;

        public Transform _groundDetection;

        private void Awake()
        {
            _creature = GetComponent<Creature>();
        }

        public override IEnumerator DoPatrol()
        {

            while (enabled)
            {
                RaycastHit2D groundCheckInfo = Physics2D.Raycast(_groundDetection.position, Vector2.down, _rayDistance, _mask);

                if (groundCheckInfo.collider == false)
                {
                    _direction *= -1;
                }

                _creature.SetDirection(_direction);

                yield return null;
            }

        }

    }
}