using Hero;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(HeroShooter))]

    public class PlayerInput : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        private HeroShooter _heroShooter;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _heroShooter = GetComponent<HeroShooter>();
        }

        private void Update()
        {
            float horizontalDirection = Input.GetAxis(GlobalVars.HorizontalAxis);
            bool isJumpButtonPress = Input.GetButtonDown(GlobalVars.Jump);
        
            _playerMovement.Move(horizontalDirection, isJumpButtonPress);

            if (Input.GetButtonDown(GlobalVars.Fire1))
            {
                _heroShooter.Shoot();
            }
        }
    }
}