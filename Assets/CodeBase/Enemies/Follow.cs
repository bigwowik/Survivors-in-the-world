using System;
using CodeBase.Hero;
using UnityEngine;
using Zenject;

namespace CodeBase.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Follow : MonoBehaviour
    {
        private const float MinimalDistance = 1;

        [SerializeField] private float _speed = 1f;

        //public NavMeshAgent Agent;
        private Transform _heroTransform;

        private Rigidbody2D _rb;

        [Inject]
        public void Construct(HeroMove heroMove)
        {
            _heroTransform = heroMove.transform;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }


        private void FixedUpdate()
        {
            SetDestinationForAgent();
            
        }

        private void SetDestinationForAgent()
        {
            if (_heroTransform == null)
            {
                _rb.velocity = Vector2.zero;
                return;
            }

            if (HeroNotReached())
                _rb.velocity = (_heroTransform.position - transform.position).normalized * _speed * Time.fixedDeltaTime;
            else
                _rb.velocity = Vector2.zero;
        }

        private bool HeroNotReached() =>
            Vector2.Distance(transform.position, _heroTransform.position) >= MinimalDistance;
    }
}