using CodeBase.Infrastructure.Inputs;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class HeroMove : MonoBehaviour
    {
        public float MovementSpeed = 1f;

        private Rigidbody2D _characterRb;
        private IInputService _inputService;
        private Vector2 _movementVector;


        [Inject]
        private void Construct(IInputService inputService)
        {
            Debug.Log($"Construct Hero. inputService - {inputService}");
            _inputService = inputService;
        }

        private void Awake() =>
            GetAllComponents();

        private void GetAllComponents() =>
            _characterRb = GetComponent<Rigidbody2D>();

        private void FixedUpdate() =>
            UpdateMoving();

        private void UpdateMoving()
        {
            _movementVector = Vector2.zero;
            if (_inputService.Axis.sqrMagnitude > 0)
            {
                _movementVector = _inputService.Axis;
                _movementVector.Normalize();
            }
            _characterRb.MovePosition(_characterRb.position + _movementVector * (MovementSpeed * Time.fixedDeltaTime));
        }
    }
}