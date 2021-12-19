using System;
using UnityEngine;

namespace DungeonCrawler {
    [RequireComponent(typeof(Rigidbody2D))]
    public class OrbitalRigidbody : MonoBehaviour {

        [SerializeField] private Transform objectToAttachTo;
        
        [SerializeField] private float orbitalRadius;
        [SerializeField] private float orbitalSpeed;

        public float OrbitalAngle { get; set; }
        public float OrbitalRadius {
            get => orbitalRadius;
            set => orbitalRadius = value;
        }
        public float OrbitalSpeed {
            set => orbitalSpeed = value;
        }

        private Rigidbody2D _rigidbody;

        private void Awake () {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void AttachTo (Transform transform) {
            objectToAttachTo = transform;
        }

        private void FixedUpdate () {
            if (objectToAttachTo == null) return;
            
            OrbitalAngle = (OrbitalAngle + Time.deltaTime * orbitalSpeed) % 360f;
            var targetPos = (Vector2)objectToAttachTo.position;
            targetPos += Vector2.right * Mathf.Cos(Mathf.Deg2Rad * OrbitalAngle) * orbitalRadius;
            targetPos += Vector2.up * Mathf.Sin(Mathf.Deg2Rad * OrbitalAngle) * orbitalRadius;
            _rigidbody.MovePosition(targetPos);
        }

    }
}