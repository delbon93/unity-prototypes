using UnityEngine;
using UnityEngine.Events;

namespace WorldLevelSelect
{
    [CreateAssetMenu(menuName = "WorldLevelSelect/KingdomSelectItemInfo")]
    public class KingdomSelectItemInfo : ScriptableObject {
        
        [SerializeField] private string kingdomName;
        [SerializeField] private Texture previewImage;
        [SerializeField] private Vector2 globeSurfaceCoordinates;
        [SerializeField] private Quaternion displayGlobeRotation;
        [Space(20)]
        [SerializeField] private UnityEvent onSelectedCallback;

        public string KingdomName => kingdomName;
        public string KingdomNameNoWhitespace => kingdomName.Replace(" ", "");
        public Texture PreviewImage => previewImage;
        public Quaternion DisplayGlobeRotation => displayGlobeRotation;
        public UnityEvent OnSelectedCallback => onSelectedCallback;

        public Vector3 GetSurfaceNormal (float longitudeOffset = 0) {
            var lat = Mathf.Deg2Rad * globeSurfaceCoordinates.x;
            var lon = Mathf.Deg2Rad * (globeSurfaceCoordinates.y + longitudeOffset);
            return new Vector3(
                Mathf.Cos(lat) * Mathf.Cos(lon),
                Mathf.Sin(lat),
                Mathf.Cos(lat) * Mathf.Sin(lon)
            );
        }

    }
}
