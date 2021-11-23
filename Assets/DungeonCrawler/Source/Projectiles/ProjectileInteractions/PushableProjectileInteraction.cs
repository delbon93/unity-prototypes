using System;
using UnityEngine;

namespace DungeonCrawler {
    public class PushableProjectileInteraction : AProjectileInteraction {

        [SerializeField] private float pushForce;
        [SerializeField] private bool reflectProjectiles;
        [SerializeField] private float reflectedProjectileSpeedFactor;

        private ProjectileSpawner _spawner;

        private void Awake () {
            _spawner = GetComponent<ProjectileSpawner>();
        }

        public override void OnReceiveProjectile (AProjectile projectile) {
            var forceDirection = transform.position - projectile.transform.position;
            GetComponent<Rigidbody2D>().AddForce(forceDirection * pushForce);

            if (reflectProjectiles) {
                var projectileData = projectile.ProjectileData;
                var projectileSpeed = projectileData.initialVelocity.magnitude;
                projectileData.initialVelocity = -forceDirection * projectileSpeed * reflectedProjectileSpeedFactor;
                projectileData.originGameObject = gameObject;
                _spawner.SpawnProjectile(projectileData.prefab, projectileData);
            }
        }
    }
}