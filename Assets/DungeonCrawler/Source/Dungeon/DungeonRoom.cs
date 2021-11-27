using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawler
{
    public class DungeonRoom : MonoBehaviour
    {
        [SerializeField] private Grid tilemapGrid;
        [SerializeField] private Transform playerSpawn;

        public Vector3 PlayerSpawnPosition => playerSpawn.position;
        
        public void SetRoomVisible (bool visible) {
            tilemapGrid.enabled = visible;
        }

        public void Load () {
            SetRoomObjectsActiveStatus(true);
        }

        public void Unload (float timeDelayBeforeUnloading = 0.0f) {
            StartCoroutine(WaitThenUnloadCoroutine(timeDelayBeforeUnloading));
        }

        private IEnumerator WaitThenUnloadCoroutine (float timeDelayBeforeUnloading) {
            yield return new WaitForSeconds(timeDelayBeforeUnloading);
            SetRoomObjectsActiveStatus(false);
        }

        private void SetRoomObjectsActiveStatus (bool active) {
            transform.Find("Grid").gameObject.SetActive(active);
            transform.Find("Entities").gameObject.SetActive(active);
        }
    }
}
