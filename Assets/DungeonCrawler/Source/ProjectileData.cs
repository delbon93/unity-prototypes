using UnityEngine;

namespace DungeonCrawler {
    public struct ProjectileData {
        
        public Vector2 initialVelocity;
        public float size;
        public GameObject originGameObject;
        public AProjectile prefab;
        public Color color;
        
        public void ImprintOntoProjectile (AProjectile projectile) {
            projectile.ProjectileData = this;
            projectile.GetComponent<SpriteRenderer>().color = color;
            var offsetPosition = projectile.transform.position;
            offsetPosition.x += initialVelocity.normalized.x * 0.6f;
            offsetPosition.y += initialVelocity.normalized.y * 0.6f;
            projectile.transform.position = offsetPosition;
            projectile.transform.localScale *= size;
            projectile.GetComponent<Rigidbody2D>().velocity = initialVelocity;
        }

    }
}