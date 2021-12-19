using System;
using System.Collections;
using DungeonCrawler.Abilities;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

namespace DungeonCrawler {
    public class PlayerAbilityHandler : MonoBehaviour {

        [SerializeField] private RectTransform cooldownTimerSprite;
        [SerializeField] private APlayerAbility playerAbility;
        
        private bool _playerAbilityReady = true;
        private float _cooldownTimerInitialWidth;

        private void Awake () {
            _cooldownTimerInitialWidth = cooldownTimerSprite.rect.width;
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
            cooldownTimerSprite.sizeDelta =
                new Vector2(_cooldownTimerInitialWidth * scale, cooldownTimerSprite.sizeDelta.y);
        }

        public void OnUsePlayerAbility (InputAction.CallbackContext context) {
            if (!context.performed) return;
            UsePlayerAbility();
        }
    }
}