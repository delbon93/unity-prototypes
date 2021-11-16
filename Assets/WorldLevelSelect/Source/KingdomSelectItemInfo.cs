using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldLevelSelect
{
    [CreateAssetMenu(menuName = "WorldLevelSelect/KingdomSelectItemInfo")]
    public class KingdomSelectItemInfo : ScriptableObject {
        
        [SerializeField] private string kingdomName;
        [SerializeField] private Texture previewImage;
        [SerializeField] private Vector3 locationOnGlobe;
        [SerializeField] private Quaternion displayGlobeRotation;

        public string KingdomName => kingdomName;
        public string KingdomNameNoWhitespace => kingdomName.Replace(" ", "");
        public Texture PreviewImage => previewImage;
        public Vector3 LocationOnGlobe => locationOnGlobe;
        public Quaternion DisplayGlobeRotation => displayGlobeRotation;

    }
}
