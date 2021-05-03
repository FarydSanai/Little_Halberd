using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class Projectile : MonoBehaviour
    {
        private float gravityMult;
        public float GravitiMultimplier
        {
            get
            {
                if (gravityMult == 0)
                {
                    gravityMult = Mathf.Sqrt(this.GetComponent<Rigidbody2D>().gravityScale);
                }
                return gravityMult;
            }
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<EnemyAI>() == null)
            {
                Destroy(this.gameObject);
            }
        }
    }
}