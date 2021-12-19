using System;
using UnityEngine;

namespace DungeonCrawler {
    [RequireComponent(typeof(ProjectileSpawner))]
    public class ReflectProjectileInteraction : AProjectileInteraction {
        
        [SerializeField] private float reflectedProjectileSpeedFactor;
        [SerializeField] private float minimumReflectedVelocity;
        

        private ProjectileSpawner _projectileSpawner;

        private void Awake () {
            _projectileSpawner = GetComponent<ProjectileSpawner>();
        }

        protected override void OnReceiveProjectile (AProjectile projectile) {
            var forceDirection = transform.position - projectile.transform.position;
            
            var projectileAttributes = projectile.ProjectileAttributes;
            var projectileSpeed = Mathf.Max(projectileAttributes.RecentVelocity.magnitude, minimumReflectedVelocity);
            projectileAttributes.InitialVelocity = -forceDirection * projectileSpeed * reflectedProjectileSpeedFactor;
            projectileAttributes.OriginGameObject = gameObject;
            _projectileSpawner.SpawnProjectileFromPrefab(projectileAttributes.Prefab, projectileAttributes);
        }
        
    }
}