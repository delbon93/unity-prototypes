using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DungeonCrawler
{
    public class PlayerMovement : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("speed");
        
        [SerializeField] private float moveSpeed;
        [SerializeField] private SpriteRenderer sprite;

        private Rigidbody2D _rigidbody;
        private Vector2 _movementVector;

        private void Awake () {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate () {
            _rigidbody.AddForce(_movementVector * moveSpeed);
            GetComponent<Animator>().SetFloat(Speed, _movementVector.magnitude);
            if (sprite.flipX && _movementVector.x > 0) sprite.flipX = false;
            if (!sprite.flipX && _movementVector.x < 0) sprite.flipX = true;
        }

        public void OnMove (InputAction.CallbackContext context) {
            _movementVector = context.ReadValue<Vector2>();
        }
    }
}
