using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DungeonCrawler
{
    public class PlayerMovement : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("speed");
        
        [SerializeField] private float moveSpeed;
        [SerializeField] private float fireDelay;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private SpriteRenderer sprite;

        private static readonly int Hurt = Animator.StringToHash("hurt");
        private Rigidbody2D _rigidbody;
        private ProjectileSpawner _projectileSpawner;
        private Vector2 _movementVector;
        private Vector2 _fireProjectileVector;

        public bool InputEnabled { get; set; } = true;

        private void Awake () {
            _rigidbody = GetComponent<Rigidbody2D>();
            _projectileSpawner = GetComponent<ProjectileSpawner>();
            StartCoroutine(FireProjectileCoroutine());
        }
        
        private void FixedUpdate () {
            if (!InputEnabled) return;
            
            _rigidbody.AddForce(_movementVector * moveSpeed);
            GetComponent<Animator>().SetFloat(Speed, _movementVector.magnitude);
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
            GetComponent<AudioSource>().Play();
            var projectileData = new ProjectileData {
                initialVelocity = _fireProjectileVector.normalized * projectileSpeed,
                size = 2,
                originGameObject = gameObject,
                prefab = _projectileSpawner.ProjectilePrefab,
                color = Color.magenta
            };
            _projectileSpawner.SpawnProjectile(projectileData);
        }

        public void OnMove (InputAction.CallbackContext context) {
            _movementVector = context.ReadValue<Vector2>();
        }

        public void OnFireProjectile (InputAction.CallbackContext context) {
            _fireProjectileVector = context.ReadValue<Vector2>();
        }

        public void OnReceiveDamage () {
            GetComponent<Animator>().SetTrigger(Hurt);
        }
    }
}
