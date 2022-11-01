using UnityEngine;

namespace CodeBase.Infrastructure.Service.Input
{
    [CreateAssetMenu(fileName = "PlayerController", menuName = "Source/PlayerController")]
    public class PlayerController : Controller
    {
        private InputService _playerInput;

        public override void Init() => 
            _playerInput = FindObjectOfType<InputService>();

        public override void OnCharacterUpdate() =>
            Character.SetMovementInput(GetMovementInput());

        private Vector3 GetMovementInput()
        {
            Quaternion yawRotation = Quaternion.Euler(0.0f, Character.GetControlRotation().y, 0.0f);
            Vector3 forward = yawRotation * Vector3.forward;
            Vector3 right = yawRotation * Vector3.right;
            Vector3 movementInput = (forward * _playerInput.MoveInput.y + right * _playerInput.MoveInput.x);
            
            if (movementInput.sqrMagnitude > 1f) 
                movementInput.Normalize();

            return movementInput;
        }
    }
}