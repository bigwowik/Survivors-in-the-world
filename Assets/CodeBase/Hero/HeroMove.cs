﻿using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class HeroMove : MonoBehaviour
    {
        public Rigidbody2D CharacterRb; //higher performance by find in awake
        public float MovementSpeed = 1f;
        private IInputService _inputService;
        private Camera _camera;
        private Vector2 movementVector;


        [Inject]
        private void Construct(IInputService inputService)
        {
            Debug.Log($"Construct Hero. inputService - {inputService}");
            _inputService = inputService;
        }
        
        
        private void Awake()
        {
            GetAllComponents();
            
            
            //_inputService = AllServices.Container.Single<IInputService>();
        }

        private void GetAllComponents()
        {
            CharacterRb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void OnDestroy()
        {
            Debug.Log("Destroy hero");
        }

        private void FixedUpdate()
        {
            UpdateMoving();
              
        }

        private void UpdateMoving()
        {
            movementVector = Vector2.zero;
            if (_inputService.Axis.sqrMagnitude > 0) // Constants.Epsilon)
            {
                movementVector = _inputService.Axis;
                movementVector.Normalize();
            }
            CharacterRb.MovePosition(CharacterRb.position + movementVector * (MovementSpeed * Time.fixedDeltaTime));
        }
    }
}