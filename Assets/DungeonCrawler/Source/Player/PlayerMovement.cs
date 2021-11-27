using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DungeonCrawler
{
    public class PlayerMovement : MonoBehaviour
    {
        private static readonly int SpeedParameter = Animator.StringToHash("speed");
        private static readonly int HurtParameter = Animator.StringToHash("hurt");
        
        [SerializeField] private float moveSpeed;
        [SerializeField] private float fireDelay;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private SpriteRenderer sprite;

        private Rigidbody2D _rigidbody;
        private ProjectileSpawner _projectileSpawner;
        private Animator _animator;
        private AudioSource _audioSource;
        
        private Vector2 _movementVector;
        private Vector2 _fireProjectileVector;

        public bool InputEnabled { get; set; } = true;

        private void Awake () {
            _rigidbody = GetComponent<Rigidbody2D>();
            _projectileSpawner = GetComponent<ProjectileSpawner>();
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            StartCoroutine(FireProjectileCoroutine());
        }
        
        private void FixedUpdate () {
            if (!InputEnabled) return;
            
            _rigidbody.AddForce(_movementVector * moveSpeed);
            _animator.SetFloat(SpeedParameter, _movementVector.magnitude);
            if (sprite.flipX && _movementVector.x > 0) sprite.flipX = false;
            if (!sprite.flipX && _movementVector.x < 0) sprite.flipX = true;
        }

        private IEnumerator FireProjectileCoroutine () {
            for (;;) {
                if (_fireProjectileVector.magnitude < Mathf.Epsilon || !InputEnabled) {
                    yield return new WaitForEndOfFrame();
                } 
                else {
                    SpawnProjectile();
                    yield return new WaitForSeconds(fireDelay);
                }
            }
        }

        private void SpawnProjectile () {
            _audioSource.Play();
            var projectileAttributes = new ProjectileAttributes {
                InitialVelocity = _fireProjectileVector.normalized * projectileSpeed,
                Size = 2,
                OriginGameObject = gameObject,
                Prefab = _projectileSpawner.ProjectilePrefab,
                Color = Color.magenta
            };
            _projectileSpawner.SpawnProjectile(projectileAttributes);
        }

        public void OnMove (InputAction.CallbackContext context) {
            _movementVector = context.ReadValue<Vector2>();
        }

        public void OnFireProjectile (InputAction.CallbackContext context) {
            _fireProjectileVector = context.ReadValue<Vector2>();
        }

        public void OnReceiveDamage () {
            _animator.SetTrigger(HurtParameter);
        }
    }
}
