using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawler
{
    public class ProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private Projectile projectilePrefab;

        private Transform _projectileContainer;

        private void Awake () {
            _projectileContainer = GameObject.FindGameObjectWithTag("ProjectileContainer").transform;
        }

        public void SpawnProjectile (ProjectileData projectileData) {
            var projectile = Instantiate(projectilePrefab, _projectileContainer);
            projectile.ProjectileData = projectileData;
            var offsetPosition = transform.position;
            offsetPosition.x += projectileData.initialVelocity.normalized.x * 0.6f;
            offsetPosition.y += projectileData.initialVelocity.normalized.y * 0.6f;
            projectile.transform.position = offsetPosition;
            projectile.transform.localScale *= projectileData.size;
            projectile.GetComponent<Rigidbody2D>().velocity = projectileData.initialVelocity;
        }
    }
}
