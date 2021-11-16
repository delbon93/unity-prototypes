using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldLevelSelect
{
    public class UIPreviewImage : MonoBehaviour
    {
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");

        public void SetPreviewTexture (Texture texture) {
            GetComponent<MeshRenderer>().material.SetTexture(MainTex, texture);
        }
    }
}
