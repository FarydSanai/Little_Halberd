using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private GameObject ExplodeVFX;
        [SerializeField] private GameObject RepelVFX;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<CharacterAI>() == null)
            {
                Instantiate(ExplodeVFX, this.transform.position, Quaternion.identity);
                PoolObjectLoader.Instance.DestroyObject(this.gameObject);
            }
        }
    }
}