using System;
using System.Collections;
using DungeonCrawler.Abilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DungeonCrawler {
    public class PlayerAbilityHandler : MonoBehaviour {

        [SerializeField] private SpriteRenderer cooldownTimerSprite;
        [SerializeField] private APlayerAbility playerAbility;
        
        private bool _playerAbilityReady = true;

        private void Awake () {
            SetSpriteScale(0);
        }

        public void UsePlayerAbility () {
            if (!_playerAbilityReady || playerAbility == null) return;
            
            playerAbility.OnActivate(this);
            StartCoroutine(PlayerAbilityCooldownTimer());
        }

        private IEnumerator PlayerAbilityCooldownTimer () {
            SetSpriteScale(1);
            _playerAbilityReady = false;
            
            var remainingTime = playerAbility.CooldownTime;
            while (remainingTime > 0) {
                yield return new WaitForFixedUpdate();
                remainingTime -= Time.deltaTime;
                SetSpriteScale(remainingTime / playerAbility.CooldownTime);
            }
            
            SetSpriteScale(0);
            _playerAbilityReady = true;
        }

        private void SetSpriteScale (float scale) {
            var localScale = cooldownTimerSprite.transform.localScale;
            localScale.x = scale;
            cooldownTimerSprite.transform.localScale = localScale;
        }

        public void OnUsePlayerAbility (InputAction.CallbackContext context) {
            if (!context.performed) return;
            UsePlayerAbility();
        }
    }
}