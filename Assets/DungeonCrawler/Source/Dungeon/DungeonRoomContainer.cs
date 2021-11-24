using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawler
{
    public class DungeonRoomContainer : MonoBehaviour
    {

        [SerializeField] private DungeonRoom initiallyLoadedRoom;
        [SerializeField] private PlayerMovement player;

        private void Start () {
            ResetDungeon();
        }

        public void ResetDungeon () {
            SetActiveRoom(initiallyLoadedRoom.gameObject);
            player.transform.position = initiallyLoadedRoom.PlayerSpawnPosition;
            FindObjectOfType<ProjectileContainer>()?.ClearAllProjectiles();
        }

        public void SetActiveRoom (GameObject roomGameObject) {
            foreach (Transform child in transform) {
                var dungeonRoom = child.GetComponent<DungeonRoom>();
                if (dungeonRoom == null) continue;
                
                if (dungeonRoom.gameObject == roomGameObject) {
                    dungeonRoom.Load();
                }
                else {
                    dungeonRoom.Unload();
                }
            }
        }

    }
}
