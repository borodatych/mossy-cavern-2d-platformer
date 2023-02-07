using System.Collections.Generic;
using Bullet;
using UnityEngine;

namespace Hero
{
    public class HeroShooter : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _spawn;
        [SerializeField] private float _speed;

        private BulletSpawn _script;
        private List<Collider2D> _childrenColliders;

        private void Awake()
        {
            _script = _spawn.gameObject.GetComponent<BulletSpawn>();
        }

        private void Start()
        {
            _childrenColliders = new List<Collider2D>();
            if (_root)
            {
                AddChildrenColliders(_root.transform);
            }
        }

        private void AddChildrenColliders(Transform t)
        {
            for (int i = 0 ; i < t.childCount ; ++i)
            {
                Transform child = t.GetChild(i);
                AddChildrenColliders(child);

                Collider2D c = child.GetComponent<Collider2D>();
                if (c != null)
                {
                    _childrenColliders.Add(c);
                }
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void Shoot()
        {
            GameObject currentBullet = Instantiate(_bullet, _spawn.position, Quaternion.identity);
            Rigidbody2D rbCurrentBullet = currentBullet.GetComponent<Rigidbody2D>();

            if (rbCurrentBullet != null)
            {
                currentBullet.GetComponent<BulletTrigger>().IgnoreColliders = _childrenColliders;
                currentBullet.GetComponent<HeroDamager>().IgnoreColliders = _childrenColliders;

                int dir = _script.MovingForward ? 1 : -1;
                rbCurrentBullet.velocity = new Vector2(_speed * dir, rbCurrentBullet.velocity.y);
            }
        }
    }
}
