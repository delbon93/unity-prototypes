using System;
using System.Linq;
using UnityEngine;

namespace DungeonCrawler
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public abstract class AProjectile : MonoBehaviour {

        [SerializeField] protected ParticleSystem onDestroyParticleSystem;
        [SerializeField] protected ParticleSystem trailParticleSystem;

        private ProjectileAttributes _projectileAttributes;
        
        public ProjectileAttributes ProjectileAttributes { get => _projectileAttributes; set => _projectileAttributes = value; }

        protected abstract bool AutomaticallyDestroyOnHit { get; }
        protected abstract bool ShowTrailParticlesOnStart { get; }
        

        private void Start () {
            if (ShowTrailParticlesOnStart)
                trailParticleSystem.Play();
        }

        private void FixedUpdate () {
            _projectileAttributes.RecentVelocity = GetComponent<Rigidbody2D>().velocity;
        }

        private void OnTriggerEnter2D (Collider2D other) {
            if (other.gameObject == ProjectileAttributes.OriginGameObject) return;

            OnBeforeParticleHit(); // template method
            StopAndHideProjectile();
            PlayProjectileHitSound();
            OnParticleHit(other.gameObject); // template method
            HandleProjectileInteractionsInOrder(other.gameObject);
            OnAfterParticleCollision(); // template method
            if (AutomaticallyDestroyOnHit)
                DestroyParticleAfterParticlesFinished();
        }

        protected abstract void OnBeforeParticleHit ();

        protected abstract void OnParticleHit (GameObject hitObject);

        protected abstract void OnAfterParticleCollision ();

        private void StopAndHideProjectile () {
            GetComponent<Rigidbody2D>().simulated = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }

        private void PlayProjectileHitSound () {
            GetComponent<AudioSource>().Play();
        }

        private void HandleProjectileInteractionsInOrder (GameObject hitObject) {
            var interactions = hitObject.GetComponents<AProjectileInteraction>().ToList();
            foreach (var projectileInteraction in interactions.OrderBy(i => i.InteractionPriority)) {
                projectileInteraction.ReceiveProjectile(this);
            }
        }

        private void DestroyParticleAfterParticlesFinished () {
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
