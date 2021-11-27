namespace DungeonCrawler {
    public class OpenChestInteraction : APlayerInteraction {

        private readonly TreasureChest _treasureChest;

        public OpenChestInteraction (TreasureChest treasureChest) {
            _treasureChest = treasureChest;
        }
        
        public override void OnPlayerInteraction () {
            _treasureChest.OpenChest();
        }
    }
}