using System.Collections;
using UnityEngine;

namespace Triggers
{
    public class BulletTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!((IList) GlobalStringVars.BulletIgnoreTags).Contains(collision.gameObject.tag))
            {
                Destroy(gameObject);
            }
        }
    }
}