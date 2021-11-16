using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldLevelSelect
{
    public class WorldSphere : MonoBehaviour {

        [SerializeField] private float globeRadius;
        [SerializeField] private LocationMarker locationMarkerPrefab;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private bool smoothRotation;
        
        private List<KingdomSelectItemInfo> _itemInfos;
        private List<LocationMarker> _locationMarkers;
        private IEnumerator _currentRotation;

        public void CreateGlobeLocations (List<KingdomSelectItemInfo> itemInfos) {
            _itemInfos = itemInfos;
            _locationMarkers = new List<LocationMarker>();
            for (var i = 0; i < itemInfos.Count; i++) {
                var itemInfo = itemInfos[i];
                
                // Location markers
                var locationMarker = Instantiate(locationMarkerPrefab, transform);
                locationMarker.name = $"Location_{itemInfo.KingdomNameNoWhitespace}";
                locationMarker.transform.localPosition = itemInfo.LocationOnGlobe;
                var normal = (locationMarker.transform.position - transform.position).normalized;
                locationMarker.transform.LookAt(locationMarker.transform.position + normal);
                _locationMarkers.Add(locationMarker);

                // Splines between location markers
                if (i > 0) {
                    var targetPoint = _locationMarkers[i - 1].transform.position;
                    var midPointBase = (targetPoint + _locationMarkers[i].transform.position) / 2f;
                    var midPointNormal = (midPointBase - transform.position);
                    var midPointHeight = globeRadius - (midPointBase - transform.position).magnitude;
                    var midPoint = midPointBase + midPointNormal.normalized * 0.3f * midPointHeight;
                    
                    locationMarker.SetSpline(midPoint, targetPoint);
                }
            }
        }


        public void RotateToShowLocation (int locationIndex) {
            if (smoothRotation) {
                if (_currentRotation != null) {
                    StopCoroutine(_currentRotation);
                }

                _currentRotation = LerpRotationTo(_itemInfos[locationIndex].DisplayGlobeRotation);
                StartCoroutine(_currentRotation);
            }
            else
                transform.rotation = _itemInfos[locationIndex].DisplayGlobeRotation;

            for (var i = 0; i < _locationMarkers.Count; i++) {
                if (i == locationIndex)
                    _locationMarkers[i].StartParticleSystem();
                else
                    _locationMarkers[i].StopParticleSystem();
            }
        }

        private IEnumerator LerpRotationTo (Quaternion targetRotation) {
            
            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f) {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed);
                yield return null;
            }

            _currentRotation = null;
        }
        
    }
}
