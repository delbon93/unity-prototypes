using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonCrawler {
    [RequireComponent(typeof(AudioSource))]
    public class DefaultProjectile : AProjectile {
        
        [SerializeField] private float soundPitchVariation;

        private AudioSource _audioSource;
        
        private float _baseSoundPitch;

        protected override bool AutomaticallyDestroyOnHit => true;
        protected override bool ShowTrailParticlesOnStart => true;

        private void Awake () {
            _audioSource = GetComponent<AudioSource>();
            _baseSoundPitch = _audioSource.pitch;
        }

        protected override void OnBeforeParticleHit () {
            _audioSource.pitch = _baseSoundPitch * Random.Range(1f - soundPitchVariation, 1f + soundPitchVariation);
        }

        protected override void OnParticleHit (GameObject hitObject) { }
        protected override void OnAfterParticleCollision () { }
    }
}