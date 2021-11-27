using UnityEngine;
using UnityEngine.Events;

namespace DungeonCrawler {
    public class DamageProjectileInteraction : AProjectileInteraction {

        [SerializeField] private HealthBar healthBar;
        [Space(20)]
        [SerializeField] private UnityEvent onHealthReachesZero;
        
        protected override void OnReceiveProjectile (AProjectile projectile) {
            healthBar.ChangeHealthBy(-1);
            if (healthBar.IsHealthEmpty)
                onHealthReachesZero?.Invoke();
        }

    }
}