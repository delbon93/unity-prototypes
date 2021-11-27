using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

namespace DungeonCrawler {
    public class RoomTransitionInteraction : APlayerInteraction {

        private readonly RoomTransition _from;
        private readonly RoomTransition _to;
        private readonly GameObject _player;

        public RoomTransitionInteraction (RoomTransition from, RoomTransition to, GameObject player) {
            _from = from;
            _to = to;
            _player = player;
        }
        
        public override void OnPlayerInteraction () {
            _from.DungeonRoom.Unload(1.0f);
            _to.DungeonRoom.Load();
            _player.transform.position = _to.SpawnPointPosition;
            _player.GetComponent<PlayerCameraManager>()?.SetFocusOnRoom(_to.DungeonRoom);
            GameObject.FindObjectOfType<ProjectileContainer>().ClearAllProjectiles();
        }
        
    }
}