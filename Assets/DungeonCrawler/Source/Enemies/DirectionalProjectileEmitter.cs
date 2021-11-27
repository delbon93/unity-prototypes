using System;
using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands.Annotate;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DungeonCrawler
{
    [RequireComponent(typeof(ProjectileSpawner))]
    public class DirectionalProjectileEmitter : MonoBehaviour
    {
        [SerializeField] private float fireDelay;
        [SerializeField] private Vector2 fireDirection;
        [SerializeField] private float maxDistanceToFireAtPlayer;
        [SerializeField] private ParticleSystem glowingEyesParticleSystem;
        
        
        private ProjectileSpawner _spawner;

        private void Awake () {
            _spawner = GetComponent<ProjectileSpawner>();
        }

        private void OnEnable () {
            StartCoroutine(SpawnProjectileAfterDelay());
        }

        private IEnumerator SpawnProjectileAfterDelay () {
            for (;;) {
                yield return new WaitForSeconds(fireDelay);

                var playerTarget = GetClosestPlayerInRange();

                var velocity = fireDirection;
                
                if (playerTarget != null) {
                    velocity = (playerTarget.transform.position - transform.position).normalized *
                               fireDirection.magnitude;
                    glowingEyesParticleSystem.Play();
                }
                else {
                    glowingEyesParticleSystem.Stop();
                    glowingEyesParticleSystem.Clear();
                }
                
                var projectileData = new ProjectileData() {
                    color = Color.cyan,
                    prefab = _spawner.ProjectilePrefab,
                    initialVelocity = velocity,
                    originGameObject = gameObject,
                    size = 1.5f
                };
                
                _spawner.SpawnProjectile(projectileData);
            }
        }

        private PlayerMovement GetClosestPlayerInRange () {
            var closestDist = float.MaxValue;
            PlayerMovement closestPlayer = null;
            foreach (var player in FindObjectsOfType<PlayerMovement>()) {
                var dist = (player.transform.position - transform.position).magnitude; 
                if (dist < maxDistanceToFireAtPlayer && dist < closestDist) {
                    closestDist = dist;
                    closestPlayer = player;
                }
            }

            return closestPlayer;
        }

        public void OnDeath () {
            Destroy(gameObject);
        }
        
#if UNITY_EDITOR

        private void OnDrawGizmos () {
            Gizmos.DrawWireSphere(transform.position, maxDistanceToFireAtPlayer);
        }

#endif
        
    }
}
