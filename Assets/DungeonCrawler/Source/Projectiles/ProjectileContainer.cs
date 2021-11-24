using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawler
{
    public class ProjectileContainer : MonoBehaviour
    {
        public void ClearAllProjectiles () {
            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
