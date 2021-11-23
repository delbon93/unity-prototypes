using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonCrawler {
    [RequireComponent(typeof(AudioSource))]
    public class DefaultProjectile : AProjectile {
        
        [SerializeField] private float soundPitchVariation;

        private float _baseSoundPitch;

        protected override bool AutomaticallyDestroyOnHit => true;
        protected override bool ShowTrailParticlesOnStart => true;

        private void Awake () {
            _baseSoundPitch = GetComponent<AudioSource>().pitch;
        }

        protected override void OnBeforeParticleHit () {
            GetComponent<AudioSource>().pitch = _baseSoundPitch 
                                                * Random.Range(1f - soundPitchVariation, 1f + soundPitchVariation);
        }

        protected override void OnParticleHit (GameObject hitObject) { }
        protected override void OnAfterParticleCollision () { }
    }
}