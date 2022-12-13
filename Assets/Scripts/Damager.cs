using System.Collections;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private bool _instant;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( ((IList) GlobalStringVars.MortalTags).Contains(collision.gameObject.tag) )
        {
            collision.gameObject.GetComponent<Health>()?.TakeDamage(_damage, _instant);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( ((IList) GlobalStringVars.MortalTags).Contains(collision.gameObject.tag) )
        {
            collision.gameObject.GetComponent<Health>()?.TakeDamage(_damage, _instant);
        }
    }
}
