using System;
using System.Collections;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private bool _instant;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.LogFormat($" >> TRIGGER ::: {name} => {collision.gameObject.tag} {_damage}");
        if ( ((IList) GlobalStringVars.MortalTags).Contains(collision.gameObject.tag) )
        {
            //Debug.LogFormat($"{name} => {collision.gameObject.tag} {_damage}");
            collision.gameObject.GetComponent<Health>()?.TakeDamage(_damage, _instant);
        }

        // Если это пуля, уничтожаем ее, чтобы не копилось
        if (CompareTag("Bullet") && !((IList) GlobalStringVars.BulletIgnoreTags).Contains(collision.gameObject.tag))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.LogFormat($" >> COLLISION ::: {name} => {collision.gameObject.tag} {_damage}");
        if ( ((IList) GlobalStringVars.MortalTags).Contains(collision.gameObject.tag) )
        {
            //Debug.LogFormat($"{name} => {collision.gameObject.tag} {_damage}");
            collision.gameObject.GetComponent<Health>()?.TakeDamage(_damage, _instant);
        }
    }
}
