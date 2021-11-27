using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util.TemplateCoroutines;

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

            StartCoroutine(new WaitThenExecuteCoroutine(1.5f, CloseChest).Start());
        }

        public void CloseChest () {
            GetComponent<SpriteRenderer>().sprite = closedChestSprite;
            GetComponent<TreasureChestInteractionProvider>().CanNoLongerBeOpened = false;
        }
        
    }
}
