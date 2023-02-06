using System.Collections;
using UnityEngine;

namespace Hero
{
    public class HeroDamager : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private bool _instant;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ( ((IList) GlobalVars.MortalTags).Contains(collision.tag) )
            {
                var root = collision.gameObject.GetComponent<HeroCollider>()?.Root 
                    ? collision.gameObject.GetComponent<HeroCollider>().Root 
                    : collision.gameObject;
                root.GetComponent<HeroHealth>()?.TakeDamage(_damage, _instant);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if ( ((IList) GlobalVars.MortalTags).Contains(collision.gameObject.tag) )
            {
                var root = collision.gameObject.GetComponent<HeroCollider>()?.Root 
                    ? collision.gameObject.GetComponent<HeroCollider>().Root 
                    : collision.gameObject;
                root.GetComponent<HeroHealth>()?.TakeDamage(_damage, _instant);
            }
        }
    }
}
