using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawler
{
    [RequireComponent(typeof(Collider2D))]
    public class DungeonRoomZone : MonoBehaviour
    {
        [SerializeField] private DungeonRoom dungeonRoom;
        

        private void OnTriggerEnter2D (Collider2D other) {
            if (other.GetComponent<PlayerCameraManager>() == null) return;
            other.GetComponent<PlayerCameraManager>().SetFocusOnRoom(dungeonRoom);
            dungeonRoom.SetRoomVisible(true);
        }

        private void OnTriggerExit2D (Collider2D other) {
            if (other.GetComponent<PlayerCameraManager>() == null) return;
            dungeonRoom.SetRoomVisible(false);
        }
    }
}
