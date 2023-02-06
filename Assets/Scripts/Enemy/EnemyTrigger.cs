using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyTrigger : MonoBehaviour
    {
        private EnemyController _controller;

        private void Awake()
        {
            _controller = gameObject.GetComponentInParent(typeof(EnemyController)) as EnemyController;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (gameObject.name == "Wall_Forward" && ((IList) GlobalVars.EnemystopOnEnter).Contains(other.gameObject.tag))
            {
                _controller.CurrentState = EnemyController.StopState;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (gameObject.name == "Ground_Forward" && other.CompareTag("Ground"))
            {
                _controller.CurrentState = EnemyController.StopState;
            }
        }
    }
}