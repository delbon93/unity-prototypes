using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace DungeonCrawler {
    public class PlayerDeathHandler : MonoBehaviour {

        [SerializeField] private HealthBar healthBar;
        [Space(20)]
        [SerializeField] private UnityEvent onBeforeDeath;
        [SerializeField] private UnityEvent onAfterDeath;
        
        private static readonly int Die = Animator.StringToHash("die");

        public void OnPlayerDeath () {
            StartCoroutine(PlayerDeathAnimationCoroutine());
        }

        private IEnumerator PlayerDeathAnimationCoroutine () {
            GetComponent<PlayerMovement>().InputEnabled = false;
            GetComponent<DamageProjectileInteraction>().IgnoreProjectiles = true;
            
            onBeforeDeath?.Invoke();
            
            GetComponent<Animator>().SetTrigger(Die);
            yield return new WaitForSeconds(4f);
            
            onAfterDeath?.Invoke();
            
            healthBar.SetFullHealth();
            GetComponent<PlayerMovement>().InputEnabled = true;
            GetComponent<DamageProjectileInteraction>().IgnoreProjectiles = false;
        }
        
    }
}