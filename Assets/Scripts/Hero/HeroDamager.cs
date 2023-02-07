using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hero
{
    public class HeroDamager : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private bool _instant;
        private List<Collider2D> _ignoreColliders;

        public List<Collider2D> IgnoreColliders {
            get => _ignoreColliders;
            set => _ignoreColliders = value;
        }

        private void Awake()
        {
            _ignoreColliders = new List<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!IsChildCollider(collision) && ((IList) GlobalVars.MortalTags).Contains(collision.tag))
            {
                var root = collision.gameObject.GetComponent<HeroCollider>()?.Root 
                    ? collision.gameObject.GetComponent<HeroCollider>().Root 
                    : collision.gameObject;
                root.GetComponent<HeroHealth>()?.TakeDamage(_damage, _instant);
            }
        }
        private bool IsChildCollider(Collider2D collision)
        {
            foreach (var t in IgnoreColliders)
            {
                if (collision.CompareTag(t.tag)) return true;
            }

            return false;
        }
    }
}
