using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyTrigger : MonoBehaviour
    {
        private EnemyMovement _movement;

        private void Awake()
        {
            _movement = gameObject.GetComponentInParent(typeof(EnemyMovement)) as EnemyMovement;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (gameObject.name == "Wall_Forward" && ((IList) GlobalVars.EnemyStopOnEnter).Contains(other.gameObject.tag))
            {
                _movement.CurrentState = EnemyMovement.StopState;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (gameObject.name == "Ground_Forward" && other.CompareTag("Ground"))
            {
                _movement.CurrentState = EnemyMovement.StopState;
            }
        }
    }
}