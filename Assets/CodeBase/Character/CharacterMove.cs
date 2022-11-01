using CodeBase.Infrastructure.Service.Input;
using UnityEngine;

namespace CodeBase.Character
{
    public class CharacterMove : MonoBehaviour
    {
        [SerializeField] private Controller Controller;

        [SerializeField] private float _acceleration = 25f;
        [SerializeField] private float _maxHorizontalSpeed = 8.0f;

        [SerializeField] private float _minRotationSpeed = 600.0f;
        [SerializeField] private float _maxRotationSpeed = 1200.0f;

        private CharacterController _characterController;

        private float _targetHorizontalSpeed;
        private float _horizontalSpeed;
        private bool _justWalkedOffALedge;

        private Vector2 _controlRotation;
        private Vector3 _movementInput;
        private Vector3 _lastMovementInput;
        private bool _hasMovementInput;


        private float _eulerY;


        private void Awake()
        {
            Controller.Init();
            Controller.Character = this;

            _characterController = GetComponent<CharacterController>();
        }

        private void Update() =>
            Controller.OnCharacterUpdate();

        private void FixedUpdate() =>
            Tick(Time.deltaTime);

        private void Tick(float deltaTime)
        {
            UpdateHorizontalSpeed(deltaTime);

            Vector3 movement = _horizontalSpeed * GetMovementDirection();
            _characterController.Move(movement * deltaTime);

            OrientToRotation(movement, deltaTime);
        }

        public void SetMovementInput(Vector3 movementInput)
        {
            bool hasMovementInput = movementInput.sqrMagnitude > 0.0f;

            if (_hasMovementInput && !hasMovementInput)
            {
                _lastMovementInput = _movementInput;
            }

            _movementInput = movementInput;
            _hasMovementInput = hasMovementInput;
        }


        private void UpdateHorizontalSpeed(float deltaTime)
        {
            Vector3 movementInput = _movementInput;
            if (movementInput.sqrMagnitude > 1.0f)
                movementInput.Normalize();

            _targetHorizontalSpeed = movementInput.magnitude * _maxHorizontalSpeed;

            _horizontalSpeed = Mathf.MoveTowards(_horizontalSpeed, _targetHorizontalSpeed,
                _acceleration * deltaTime);
        }

        private Vector3 GetMovementDirection()
        {
            Vector3 moveDir = _hasMovementInput ? _movementInput : _lastMovementInput;

            if (moveDir.sqrMagnitude > 1f)
                moveDir.Normalize();

            return moveDir;
        }

        private void OrientToRotation(Vector3 horizontalMovement, float deltaTime)
        {
            if (horizontalMovement.sqrMagnitude > 0.0f)
            {
                float rotationSpeed = Mathf.Lerp(
                    _maxRotationSpeed, _minRotationSpeed, _horizontalSpeed / _targetHorizontalSpeed);

                Quaternion targetRotation = Quaternion.LookRotation(horizontalMovement, Vector3.up);

                transform.rotation =
                    Quaternion.RotateTowards(transform.rotation, targetRotation,
                        rotationSpeed * deltaTime);
            }
        }

        public Vector2 GetControlRotation() =>
            _controlRotation;
    }
}