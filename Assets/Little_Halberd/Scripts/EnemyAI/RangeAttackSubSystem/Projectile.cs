using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class Projectile : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<EnemyAI>() == null)
            {
                //Destroy(this.gameObject);
                PoolObjectLoader.Instance.DestroyObject(this.gameObject);
            }
        }
    }
}