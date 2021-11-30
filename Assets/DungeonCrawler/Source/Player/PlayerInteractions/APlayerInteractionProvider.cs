using UnityEngine;

namespace DungeonCrawler {
    public abstract class APlayerInteractionProvider : MonoBehaviour {
        
        protected void OnTriggerEnter2D (Collider2D other) {
            if (other.gameObject.TryGetComponent(typeof(PlayerInteractionHandler), out var interactionHandler)) {
                OnPlayerEntersTrigger(interactionHandler as PlayerInteractionHandler);
            }
        }

        protected void OnTriggerExit2D (Collider2D other) {
            if (other.gameObject.TryGetComponent(typeof(PlayerInteractionHandler), out var interactionHandler)) {
                OnPlayerLeavesTrigger(interactionHandler as PlayerInteractionHandler);
            }
        }

        protected abstract void OnPlayerEntersTrigger (PlayerInteractionHandler interactionHandler);
        protected abstract void OnPlayerLeavesTrigger (PlayerInteractionHandler interactionHandler);

    }
}