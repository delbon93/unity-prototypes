using UnityEngine;

namespace DungeonCrawler {
    public abstract class AProjectileInteraction : MonoBehaviour {

        public bool IgnoreProjectiles { get; set; } = false;

        public void ReceiveProjectile (AProjectile projectile) {
            if (!IgnoreProjectiles)
                OnReceiveProjectile(projectile);
        }
        
        protected abstract void OnReceiveProjectile (AProjectile projectile);

    }
}