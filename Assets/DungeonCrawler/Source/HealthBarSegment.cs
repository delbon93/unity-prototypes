using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawler
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class HealthBarSegment : MonoBehaviour
    {
        [SerializeField] private Sprite emptyHealthSegmentSprite;
        [SerializeField] private Sprite fullHealthSegmentSprite;

        public void SetEmpty () {
            GetComponent<SpriteRenderer>().sprite = emptyHealthSegmentSprite;
        }

        public void SetFull () {
            GetComponent<SpriteRenderer>().sprite = fullHealthSegmentSprite;
        }

        public void SetSpriteAlpha (float alpha) {
            var col = GetComponent<SpriteRenderer>().color;
            col.a = alpha;
            GetComponent<SpriteRenderer>().color = col;
        }
        
    }
}
