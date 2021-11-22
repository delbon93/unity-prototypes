using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonCrawler
{
    public class Projectile : MonoBehaviour {
        public ProjectileData ProjectileData { get; set; }

        [SerializeField] private ParticleSystem onDestroyParticleSystem;
        [SerializeField] private ParticleSystem trailParticleSystem;
        [SerializeField] private float soundPitchVariation;
        


        private void Start () {
            trailParticleSystem.Play();
        }

        private void OnTriggerEnter2D (Collider2D other) {
            OnParticleHit(other.gameObject);
        }

        private void OnParticleHit (GameObject hitObject) {
            if (hitObject == ProjectileData.originGameObject) return;
            
            StopAndHideProjectile();
            PlayProjectileHitSound();
            AfterHandledCollision();
        }

        private void StopAndHideProjectile () {
            GetComponent<Rigidbody2D>().simulated = false;
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(trailParticleSystem.gameObject);
        }

        private void PlayProjectileHitSound () {
            var audioSource = GetComponent<AudioSource>();
            audioSource.pitch *= Random.Range(1f - soundPitchVariation, 1f + soundPitchVariation);
            audioSource.Play();
        }

        private void AfterHandledCollision () {
            onDestroyParticleSystem.Play();
            
            Destroy(gameObject, (onDestroyParticleSystem != null) ? onDestroyParticleSystem.main.duration : 0);
        }
    }
}
