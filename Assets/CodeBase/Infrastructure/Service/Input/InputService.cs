using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Infrastructure.Service.Input
{
    public class InputService : MonoBehaviour
    {
        public Vector2 MoveInput { get; private set; }
        public Vector2 LastMoveInput { get; private set; }
        public bool HasMoveInput{ get; private set; }

        public void OnMoveEvent(InputAction.CallbackContext context)
        {
            Vector2 moveInput = context.ReadValue<Vector2>();

            bool hasMoveInput = moveInput.sqrMagnitude > 0.0f;

            if (HasMoveInput && !hasMoveInput)
                LastMoveInput = MoveInput;

            MoveInput = moveInput;
            HasMoveInput = hasMoveInput;
        }
    }
}