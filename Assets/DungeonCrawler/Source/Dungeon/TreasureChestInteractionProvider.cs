using Newtonsoft.Json.Serialization;
using UnityEngine;

namespace DungeonCrawler {
    
    [RequireComponent(typeof(TreasureChest))]
    public class TreasureChestInteractionProvider : APlayerInteractionProvider {

        private OpenChestInteraction _openChestInteraction;
        
        public bool CanNoLongerBeOpened { get; set; } = false;
        
        protected override void OnPlayerEntersTrigger (PlayerInteractionHandler interactionHandler) {
            if (CanNoLongerBeOpened) return;
            
            _openChestInteraction = new OpenChestInteraction(GetComponent<TreasureChest>());
            interactionHandler.AllowInteraction(_openChestInteraction);
        }

        protected override void OnPlayerLeavesTrigger (PlayerInteractionHandler interactionHandler) {
            interactionHandler.DisallowInteraction(_openChestInteraction);
        }
    }
    
}