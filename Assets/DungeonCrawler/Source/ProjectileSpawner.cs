using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawler
{
    public class ProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private AProjectile projectilePrefab;

        private Transform _projectileContainer;

        public AProjectile ProjectilePrefab => projectilePrefab;

        private void Awake () {
            _projectileContainer = GameObject.FindGameObjectWithTag("ProjectileContainer").transform;
        }

        public void SpawnProjectile (ProjectileData projectileData) {
            SpawnProjectile(projectilePrefab, projectileData);
        }

        public void SpawnProjectile (AProjectile prefab, ProjectileData projectileData) {
            var projectile = Instantiate(prefab, _projectileContainer);
            projectile.transform.position = transform.position;
            projectileData.ImprintOntoProjectile(projectile);
        }

    }
}
