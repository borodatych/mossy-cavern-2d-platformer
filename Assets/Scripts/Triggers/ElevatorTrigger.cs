using UnityEngine;

namespace Triggers
{
    public class ElevatorTrigger : MonoBehaviour
    {
        private SliderJoint2D _elevator;

        private void Start()
        {
            _elevator = GetComponent<SliderJoint2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Limiter"))
            {
                Debug.LogFormat($"TagName Limiter YES");
                _elevator.useMotor = false;

                var elevatorMotor = _elevator.motor;
                elevatorMotor.motorSpeed *= -1;
                _elevator.motor = elevatorMotor;

                _elevator.useMotor = true;
            }
        }
    }
}