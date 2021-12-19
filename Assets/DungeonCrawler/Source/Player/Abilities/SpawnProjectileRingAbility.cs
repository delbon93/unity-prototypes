using System.Collections;
using PlasticPipe.PlasticProtocol.Messages;
using UnityEngine;

namespace DungeonCrawler.Abilities {
    [CreateAssetMenu(fileName = "SpawnProjectileRingAbility", menuName = "Player Abilities/Spawn Projectile Ring")]
    public class SpawnProjectileRingAbility : APlayerAbility {

        [SerializeField, Range(1, 10)] private int projectileCount;
        [SerializeField] private float ringRadius;
        [SerializeField] private float ringRotationSpeed;
        [SerializeField] private float radiateOutwardsSpeed;
        
        
        public override void OnActivate (PlayerAbilityHandler player) {
            var spawner = player.GetComponent<ProjectileSpawner>();
            
            var projectileAttributes = new ProjectileAttributes {
                InitialVelocity = Vector2.zero,
                Size = 2,
                OriginGameObject = player.gameObject,
                Prefab = spawner.ProjectilePrefab,
                Color = Color.magenta
            };

            var angleStep = 360f / projectileCount;
            for (var i = 0; i < projectileCount; i++) {
                var projectile = spawner.SpawnProjectile(projectileAttributes);
                var orbitalRigidbody = projectile.gameObject.AddComponent<OrbitalRigidbody>();
                orbitalRigidbody.OrbitalAngle = angleStep * i;
                orbitalRigidbody.OrbitalSpeed = ringRotationSpeed;
                orbitalRigidbody.AttachTo(player.transform);
                orbitalRigidbody.StartCoroutine(IncreaseProjectileOrbitalRadius(orbitalRigidbody));
            }
        }

        private IEnumerator IncreaseProjectileOrbitalRadius (OrbitalRigidbody orbitalRigidbody) {
            while (Mathf.Abs(orbitalRigidbody.OrbitalRadius - ringRadius) > 0.001f) {
                orbitalRigidbody.OrbitalRadius = Mathf.Lerp(orbitalRigidbody.OrbitalRadius, ringRadius, 
                    radiateOutwardsSpeed * Time.deltaTime);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}