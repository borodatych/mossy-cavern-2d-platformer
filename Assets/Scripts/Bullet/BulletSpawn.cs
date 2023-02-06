using UnityEngine;

namespace Bullet
{
    public class BulletSpawn: MonoBehaviour
    {
        [SerializeField] private GameObject _body;
        [SerializeField] private bool _movingForward = true;

        public bool MovingForward {
            get => _movingForward;
            set => _movingForward = value;
        }

        private void Awake()
        {
            _movingForward = _body.transform.localScale.x > 0;
        }
    }
}