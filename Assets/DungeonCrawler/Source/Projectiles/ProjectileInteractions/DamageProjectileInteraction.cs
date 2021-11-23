using UnityEngine;
using UnityEngine.Events;

namespace DungeonCrawler {
    public class DamageProjectileInteraction : AProjectileInteraction {

        [SerializeField] private HealthBar healthBar;
        [Space(20)]
        [SerializeField] private UnityEvent onHealthReachesZero;
        
        public override void OnReceiveProjectile (AProjectile projectile) {
            healthBar.HealthPoints -= 1;
            if (healthBar.HealthPoints == 0)
                onHealthReachesZero?.Invoke();
        }

    }
}