using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawler
{
    public class DungeonRoomLoader : MonoBehaviour
    {

        [SerializeField] private DungeonRoom initiallyLoadedRoom;
        [SerializeField] private PlayerMovement player;

        private void Start () {
            ResetDungeon();
        }

        public void ResetDungeon () {
            SetActiveRoom(initiallyLoadedRoom);
            player.transform.position = initiallyLoadedRoom.PlayerSpawnPosition;
            FindObjectOfType<ProjectileContainer>()?.ClearAllProjectiles();
            FindObjectOfType<PlayerCameraManager>()?.SetFocusOnRoom(initiallyLoadedRoom);
        }

        private void SetActiveRoom (DungeonRoom room) {
            foreach (Transform child in transform) {
                var dungeonRoom = child.GetComponent<DungeonRoom>();
                if (dungeonRoom == null) continue;
                
                if (dungeonRoom.gameObject == room.gameObject) {
                    dungeonRoom.Load();
                }
                else {
                    dungeonRoom.Unload();
                }
            }
        }

    }
}
