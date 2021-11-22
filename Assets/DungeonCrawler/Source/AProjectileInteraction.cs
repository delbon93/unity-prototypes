using UnityEngine;

namespace DungeonCrawler {
    public abstract class AProjectileInteraction : MonoBehaviour {

        public abstract void OnReceiveProjectile (Projectile projectile, GameObject origin);

    }
}