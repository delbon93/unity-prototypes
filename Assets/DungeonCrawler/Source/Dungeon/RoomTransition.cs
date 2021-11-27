using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonCrawler
{
    [RequireComponent(typeof(Collider2D))]
    public class RoomTransition : APlayerInteractionProvider
    {
        [SerializeField] private Transform entranceSpawnPoint;
        [SerializeField] private RoomTransition targetTransition;

        private RoomTransitionInteraction _roomTransitionInteraction;

        public DungeonRoom DungeonRoom => GetComponentInParent<DungeonRoom>();
        public Vector3 SpawnPointPosition => entranceSpawnPoint.transform.position;

        protected override void OnPlayerEntersTrigger (PlayerInteractionHandler interactionHandler) {
            _roomTransitionInteraction = new RoomTransitionInteraction(this, targetTransition, interactionHandler.gameObject);
            interactionHandler.AllowInteraction(_roomTransitionInteraction);
        }

        protected override void OnPlayerLeavesTrigger (PlayerInteractionHandler interactionHandler) {
            interactionHandler.DisallowInteraction(_roomTransitionInteraction);
        }

    }
}
