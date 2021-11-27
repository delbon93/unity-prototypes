using UnityEngine;

namespace DungeonCrawler {
    public abstract class AProjectileInteraction : MonoBehaviour {

        [Tooltip("Determines the order in which projectile interactions will be handled.")]
        [SerializeField] private int interactionPriority;
        
        public bool IgnoreProjectiles { get; set; } = false;
        public int InteractionPriority => interactionPriority;

        public void ReceiveProjectile (AProjectile projectile) {
            if (!IgnoreProjectiles)
                OnReceiveProjectile(projectile);
        }
        
        protected abstract void OnReceiveProjectile (AProjectile projectile);

    }
}