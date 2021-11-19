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
        [SerializeField] private float splineHeightBias;
        [SerializeField] private float splineHeightPerDistanceScale;
        
        
        private List<KingdomSelectItemInfo> _itemInfos;
        private List<LocationMarker> _loadedLocationMarkers;
        private IEnumerator _currentRotationCoroutine;
        private Vector3 _initialEulerRotation;

        private void Awake () {
            _initialEulerRotation = transform.rotation.eulerAngles;
        }

        public void CreateGlobeLocations (List<KingdomSelectItemInfo> itemInfos) {
            _itemInfos = itemInfos;
            _loadedLocationMarkers = new List<LocationMarker>();
            
            foreach (var itemInfo in itemInfos) {
                var locationMarker = Instantiate(locationMarkerPrefab, transform);
                locationMarker.name = $"Location_{itemInfo.KingdomNameNoWhitespace}";
                locationMarker.transform.localPosition = itemInfo.GetSurfaceNormal() * globeRadius;
                var normal = (locationMarker.transform.position - transform.position).normalized;
                locationMarker.transform.LookAt(locationMarker.transform.position + normal);
                _loadedLocationMarkers.Add(locationMarker);
            }
            
            BuildLocationConnectionSplines();
        }

        private void BuildLocationConnectionSplines () {
            if (_loadedLocationMarkers.Count < 2)
                return;
            
            for (var i = 1; i < _loadedLocationMarkers.Count; i++) {
                var targetPoint = _loadedLocationMarkers[i - 1].transform.position;
                var midPointBase = (targetPoint + _loadedLocationMarkers[i].transform.position) / 2f;
                var midPointNormal = -(midPointBase - transform.position);
                var midPointHeight = globeRadius - (midPointBase - transform.position).magnitude;
                
                // Increase height at greater distances to avoid clipping into the planet
                var locationStraightLineDistance =  (targetPoint - _loadedLocationMarkers[i].transform.position).magnitude;
                midPointHeight += splineHeightBias + splineHeightPerDistanceScale * locationStraightLineDistance;
                
                var midPoint = midPointBase + midPointNormal.normalized * midPointHeight;
                    
                _loadedLocationMarkers[i].SetSpline(midPoint, targetPoint);
            }
        }


        public void RotateToShowLocation (KingdomSelectItemInfo itemInfo) {
            if (smoothRotation) {
                if (_currentRotationCoroutine != null) {
                    StopCoroutine(_currentRotationCoroutine);
                }

                _currentRotationCoroutine = LerpRotationTo(itemInfo.DisplayGlobeRotation);
                StartCoroutine(_currentRotationCoroutine);
            } else {
                transform.rotation = itemInfo.DisplayGlobeRotation;
            }
        }

        private IEnumerator LerpRotationTo (Quaternion targetRotation) {
            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f) {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed);
                BuildLocationConnectionSplines();
                yield return null;
            }

            _currentRotationCoroutine = null;
        }
        
    }
}
