using System;
using UnityEngine;

namespace DungeonCrawler {
    
    public class AddForceProjectileInteraction : AProjectileInteraction {

        [SerializeField] private float pushForce;

        protected override void OnReceiveProjectile (AProjectile projectile) {
            var forceDirection = transform.position - projectile.transform.position;
            GetComponent<Rigidbody2D>().AddForce(forceDirection * pushForce);
        }
    }
}