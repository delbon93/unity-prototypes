using UnityEngine;

namespace DungeonCrawler.Abilities {
    public abstract class APlayerAbility : ScriptableObject, IPlayerAbility {

        [SerializeField] private float cooldownTime;

        public float CooldownTime => cooldownTime;
        
        public abstract void OnActivate (PlayerAbilityHandler player);
        
    }
}