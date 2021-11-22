using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawler
{
    public class DungeonRoom : MonoBehaviour
    {
        [SerializeField] private Grid tilemapGrid;
        
        public void SetRoomVisible (bool visible) {
            tilemapGrid.enabled = visible;
        }
    }
}
