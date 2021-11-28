using UnityEngine;

namespace DungeonCrawler
{
    [RequireComponent(typeof(TreasureChestInteractionProvider))]
    public class TreasureChest : MonoBehaviour
    {
        [SerializeField] private Sprite closedChestSprite;
        [SerializeField] private Sprite openChestSprite;

        public void OpenChest () {
            GetComponent<SpriteRenderer>().sprite = openChestSprite;
            GetComponent<AudioSource>()?.Play();
            GetComponent<TreasureChestInteractionProvider>().CanNoLongerBeOpened = true;

            Invoke(nameof(CloseChest), 1.5f);
        }

        public void CloseChest () {
            GetComponent<SpriteRenderer>().sprite = closedChestSprite;
            GetComponent<TreasureChestInteractionProvider>().CanNoLongerBeOpened = false;
        }
        
    }
}
