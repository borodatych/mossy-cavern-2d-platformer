using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(Shooter))]

    public class PlayerInput : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        private Shooter _shooter;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _shooter = GetComponent<Shooter>();
        }

        private void Update()
        {
            float horizontalDirection = Input.GetAxis(GlobalStringVars.HORIZONTAL_AXIS);
            bool isJumpButtonPress = Input.GetButtonDown(GlobalStringVars.JUMP);
        
            _playerMovement.Move(horizontalDirection, isJumpButtonPress);

            if (Input.GetButtonDown(GlobalStringVars.FIRE_1))
            {
                _shooter.Shoot();
            }
        }
    }
}