using UnityEngine;
using Util;

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
            var splineVertices = 
                Splines.GenerateCubicSpline(transform.position, midPoint, targetPoint, resolution);

            GetComponent<LineRenderer>().positionCount = splineVertices.Count;
            GetComponent<LineRenderer>().SetPositions(splineVertices.ToArray());
        }
        
    }
}
