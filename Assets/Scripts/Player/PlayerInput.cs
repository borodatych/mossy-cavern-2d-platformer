using Hero;
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
            float horizontalDirection = Input.GetAxis(GlobalVars.HorizontalAxis);
            bool isJumpButtonPress = Input.GetButtonDown(GlobalVars.Jump);
        
            _playerMovement.Move(horizontalDirection, isJumpButtonPress);

            if (Input.GetButtonDown(GlobalVars.Fire1))
            {
                _shooter.Shoot();
            }
        }
    }
}