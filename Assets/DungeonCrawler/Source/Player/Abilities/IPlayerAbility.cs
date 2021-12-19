namespace DungeonCrawler.Abilities {
    public interface IPlayerAbility {

        public float CooldownTime { get; }
        
        public void OnActivate (PlayerAbilityHandler player);
        

    }
}