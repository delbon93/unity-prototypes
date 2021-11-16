using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldLevelSelect
{
    public class StarPlane : MonoBehaviour {
        [SerializeField] private float scrollSpeed;
        
        private MeshRenderer _meshRenderer;

        private void Awake () {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Update () {
            _meshRenderer.material.mainTextureOffset += Vector2.right * scrollSpeed * Time.deltaTime;
        }
    }
}
