using UnityEngine;

namespace DungeonCrawler {
    public struct ProjectileAttributes {
        
        public Vector2 InitialVelocity;
        public Vector2 RecentVelocity;
        public float Size;
        public GameObject OriginGameObject;
        public AProjectile Prefab;
        public Color Color;
        
        public void ApplyToProjectileInstance (AProjectile projectile) {
            projectile.ProjectileAttributes = this;
            projectile.GetComponent<SpriteRenderer>().color = Color;
            var transform = projectile.transform;
            var offsetPosition = transform.position;
            offsetPosition.x += InitialVelocity.normalized.x * 0.6f;
            offsetPosition.y += InitialVelocity.normalized.y * 0.6f;
            transform.position = offsetPosition;
            transform.localScale *= Size;
            projectile.GetComponent<Rigidbody2D>().velocity = InitialVelocity;
        }

    }
}