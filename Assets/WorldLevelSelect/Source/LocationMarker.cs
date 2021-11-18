using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldLevelSelect
{
    public class LocationMarker : MonoBehaviour {
        [SerializeField] private ParticleSystem highlightParticleSystem;

        public void StartParticleSystem () {
            highlightParticleSystem.Simulate(3);
            highlightParticleSystem.Play();
        }

        public void StopParticleSystem () {
            highlightParticleSystem.Stop();
            highlightParticleSystem.Clear();
        }

        public void SetSpline (Vector3 midPoint, Vector3 targetPoint, float resolution = 0.05f) {
            // Cubic Spline Interpolation
            var startPoint = transform.position;
            var coeff1 = (startPoint - 2 * midPoint + targetPoint);
            var coeff2 = 2 * (midPoint - startPoint);
            var coeff3 = startPoint;

            var vertices = new List<Vector3>();

            for (var t = 0.0f; t < 1.0f; t += resolution) {
                var vertex = t * t * coeff1 + t * coeff2 + coeff3;
                vertices.Add(vertex);
            }
            
            vertices.Add(targetPoint);

            GetComponent<LineRenderer>().positionCount = vertices.Count;
            GetComponent<LineRenderer>().SetPositions(vertices.ToArray());
        }
        
    }
}
