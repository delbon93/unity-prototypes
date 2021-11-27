using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DungeonCrawler
{
    public class PlayerInteractionHandler : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer buttonHintSprite;

        private readonly List<APlayerInteraction> _allowedInteractions = new List<APlayerInteraction>();

        public void AllowInteraction (APlayerInteraction interaction) {
            _allowedInteractions.Add(interaction);
            UpdateInteractionHintVisibility();
        }

        public void DisallowInteraction (APlayerInteraction interaction) {
            _allowedInteractions.Remove(interaction);
            UpdateInteractionHintVisibility();
        }

        public void OnPlayerInteractionInput (InputAction.CallbackContext context) {
            if (!context.performed) return;
            ExecuteInteraction();
        }
        
        private void ExecuteInteraction () {
            if (_allowedInteractions.Count == 0) return;

            var interaction = _allowedInteractions[0];
            _allowedInteractions.RemoveAt(0);
            UpdateInteractionHintVisibility();
            interaction.OnPlayerInteraction();
        }
        
        private void UpdateInteractionHintVisibility () {
            buttonHintSprite.enabled = _allowedInteractions.Count > 0;
        }
    }
}
