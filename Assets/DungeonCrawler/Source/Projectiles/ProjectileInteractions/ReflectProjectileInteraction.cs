using System;
using UnityEngine;

namespace DungeonCrawler {
    [RequireComponent(typeof(ProjectileSpawner))]
    public class ReflectProjectileInteraction : AProjectileInteraction {
        
        [SerializeField] private float reflectedProjectileSpeedFactor;

        private ProjectileSpawner _projectileSpawner;

        private void Awake () {
            _projectileSpawner = GetComponent<ProjectileSpawner>();
        }

        protected override void OnReceiveProjectile (AProjectile projectile) {
            var forceDirection = transform.position - projectile.transform.position;
            
            var projectileAttributes = projectile.ProjectileAttributes;
            var projectileSpeed = projectileAttributes.InitialVelocity.magnitude;
            projectileAttributes.InitialVelocity = -forceDirection * projectileSpeed * reflectedProjectileSpeedFactor;
            projectileAttributes.OriginGameObject = gameObject;
            _projectileSpawner.SpawnProjectileFromPrefab(projectileAttributes.Prefab, projectileAttributes);
        }
        
    }
}