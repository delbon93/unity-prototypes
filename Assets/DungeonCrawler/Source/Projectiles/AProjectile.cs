using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonCrawler
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public abstract class AProjectile : MonoBehaviour {

        [SerializeField] protected ParticleSystem onDestroyParticleSystem;
        [SerializeField] protected ParticleSystem trailParticleSystem;

        public ProjectileData ProjectileData { get; set; }

        protected abstract bool AutomaticallyDestroyOnHit { get; }
        protected abstract bool ShowTrailParticlesOnStart { get; }


        private void Start () {
            if (ShowTrailParticlesOnStart)
                trailParticleSystem.Play();
        }

        private void OnTriggerEnter2D (Collider2D other) {
            if (other.gameObject == ProjectileData.originGameObject) return;

            OnBeforeParticleHit(); // abstract
            StopAndHideProjectile();
            PlayProjectileHitSound();
            OnParticleHit(other.gameObject); // abstract
            HandleProjectileInteraction(other.gameObject);
            OnAfterParticleCollision(); // abstract
            if (AutomaticallyDestroyOnHit)
                DestroyParticle();
        }

        protected abstract void OnBeforeParticleHit ();

        protected abstract void OnParticleHit (GameObject hitObject);

        protected abstract void OnAfterParticleCollision ();

        private void StopAndHideProjectile () {
            GetComponent<Rigidbody2D>().simulated = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }

        private void PlayProjectileHitSound () {
            var audioSource = GetComponent<AudioSource>();
            if (audioSource == null) return;
            
            audioSource.Play();
        }

        private void HandleProjectileInteraction (GameObject hitObject) {
            var interaction = hitObject.GetComponent<AProjectileInteraction>();
            if (interaction != null)
                interaction.OnReceiveProjectile(this);
        }

        private void DestroyParticle () {
            var destroyDelay = 0f;
            if (onDestroyParticleSystem != null) {
                onDestroyParticleSystem.Play();
                destroyDelay = onDestroyParticleSystem.main.duration;
            }

            gameObject.name = "(x) " + gameObject.name;

            Destroy(gameObject, destroyDelay);
        }
    }
}
