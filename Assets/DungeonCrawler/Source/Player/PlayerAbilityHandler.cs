using System.Collections;
using DungeonCrawler.Abilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DungeonCrawler {
    public class PlayerAbilityHandler : MonoBehaviour {
        
        [SerializeField] private APlayerAbility playerAbility;
        
        private bool _playerAbilityReady = true;
        
        public void UsePlayerAbility () {
            if (!_playerAbilityReady || playerAbility == null) return;
            
            print("Ability used");
            playerAbility.OnActivate(this);
            StartCoroutine(PlayerAbilityCooldownTimer());
        }

        private IEnumerator PlayerAbilityCooldownTimer () {
            _playerAbilityReady = false;
            yield return new WaitForSeconds(playerAbility.CooldownTime);
            _playerAbilityReady = true;
            print("Ability ready");
        }

        public void OnUsePlayerAbility (InputAction.CallbackContext context) {
            if (!context.performed) return;
            UsePlayerAbility();
        }
    }
}