using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace DungeonCrawler
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerCameraManager : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private float cameraMoveSpeed;
        
        private ConsistentCoroutine _moveCameraCoroutine;

        private void Awake () {
            _moveCameraCoroutine = new ConsistentCoroutine(this);
        }

        public void SetFocusOnRoom (DungeonRoom room) {
            var targetPos = room.transform.position;
            targetPos.z = playerCamera.transform.position.z;
            _moveCameraCoroutine.StartAndInterruptIfRunning(LerpCameraTowardsTargetCoroutine(targetPos));
        }

        private IEnumerator LerpCameraTowardsTargetCoroutine (Vector3 target) {
            while ((playerCamera.transform.position - target).magnitude > 0.05f) {
                var nextPosition = Vector3.Lerp(playerCamera.transform.position, target, cameraMoveSpeed);
                PointCameraAtAndKeepZ(nextPosition);
                yield return null;
            }
        }

        private void PointCameraAtAndKeepZ (Vector3 position) {
            position.z = playerCamera.transform.position.z;
            playerCamera.transform.position = position;
        }
    }
}
