using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonCrawler
{
    [RequireComponent(typeof(Collider2D))]
    public class RoomTransition : MonoBehaviour
    {
        [SerializeField] private Transform entranceSpawnPoint;
        [SerializeField] private RoomTransition targetTransition;

        private RoomTransitionInteraction _roomTransitionInteraction;

        public DungeonRoom DungeonRoom => GetComponentInParent<DungeonRoom>();
        public Vector3 SpawnPointPosition => entranceSpawnPoint.transform.position;

        private void OnTriggerEnter2D (Collider2D other) {
            if (TryGetPlayerInteractionHandler(other.gameObject, out var playerInteractionHandler)) {
                _roomTransitionInteraction = new RoomTransitionInteraction(this, targetTransition, other.gameObject);
                playerInteractionHandler.AllowInteraction(_roomTransitionInteraction);
            }
        }

        private void OnTriggerExit2D (Collider2D other) {
            if (TryGetPlayerInteractionHandler(other.gameObject, out var playerInteractionHandler)) {
                playerInteractionHandler.DisallowInteraction(_roomTransitionInteraction);
            }
        }

        private static bool TryGetPlayerInteractionHandler (GameObject playerGameObject, out PlayerInteractionHandler playerInteractionHandler) {
            playerInteractionHandler = playerGameObject.GetComponent<PlayerInteractionHandler>();
            return playerInteractionHandler != null;
        }
        
    }
}
