using System;
using System.Collections;
using System.Collections.Generic;
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
        
        
        private ProjectileSpawner _spawner;

        private void Awake () {
            _spawner = GetComponent<ProjectileSpawner>();
            StartCoroutine(SpawnProjectileAfterDelay());
        }

        private IEnumerator SpawnProjectileAfterDelay () {
            for (;;) {
                yield return new WaitForSeconds(fireDelay);
                
                var projectileData = new ProjectileData() {
                    color = Color.cyan,
                    prefab = _spawner.ProjectilePrefab,
                    initialVelocity = fireDirection,
                    originGameObject = gameObject,
                    size = 1.5f
                };
                
                _spawner.SpawnProjectile(projectileData);
            }
        }

        public void OnDeath () {
            Destroy(gameObject);
        }
        
    }
}
