using UnityEngine;

namespace Enemy
{
    public class EnemyTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("EnemyStopper"))
            {
                EnemyController.CurrentState = EnemyController.StopState;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Ground"))
            {
                EnemyController.CurrentState = EnemyController.StopState;
            }
        }
    }
}