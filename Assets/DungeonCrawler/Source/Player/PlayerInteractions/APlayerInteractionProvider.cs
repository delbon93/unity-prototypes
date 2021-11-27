using UnityEngine;

namespace DungeonCrawler {
    public abstract class APlayerInteractionProvider : MonoBehaviour {
        
        protected void OnTriggerEnter2D (Collider2D other) {
            if (TryGetPlayerInteractionHandler(other.gameObject, out var playerInteractionHandler)) {
                OnPlayerEntersTrigger(playerInteractionHandler);
            }
        }

        protected void OnTriggerExit2D (Collider2D other) {
            if (TryGetPlayerInteractionHandler(other.gameObject, out var playerInteractionHandler)) {
                OnPlayerLeavesTrigger(playerInteractionHandler);
            }
        }

        private static bool TryGetPlayerInteractionHandler (GameObject playerGameObject, out PlayerInteractionHandler playerInteractionHandler) {
            playerInteractionHandler = playerGameObject.GetComponent<PlayerInteractionHandler>();
            return playerInteractionHandler != null;
        }

        protected abstract void OnPlayerEntersTrigger (PlayerInteractionHandler interactionHandler);
        protected abstract void OnPlayerLeavesTrigger (PlayerInteractionHandler interactionHandler);

    }
}