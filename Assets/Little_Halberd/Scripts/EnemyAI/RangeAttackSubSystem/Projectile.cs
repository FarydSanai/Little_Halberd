using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private GameObject ExplodeVFX;
        [SerializeField] private GameObject RepelVFX;
        [SerializeField] private float ProjectileDamage = 100f;
        private int bitMask = (1 << 3) | (1 << 0);

        private void OnCollisionEnter2D(Collision2D other)
        {
            //Debug.Log(Convert.ToString(other.gameObject.layer, 2).PadLeft(32, '0'));
            if (other.gameObject.layer != bitMask)
            {
                SetDamage(other);
                Instantiate(ExplodeVFX, this.transform.position, Quaternion.identity);
                PoolObjectLoader.Instance.DestroyObject(this.gameObject);
            }
        }
        private void SetDamage(Collision2D other)
        {
            CharacterControl otherControl = other.gameObject.GetComponent<CharacterControl>();
            if (otherControl != null)
            {
                float dir = (other.gameObject.transform.position - this.transform.position).x;

                if (dir > 0f)
                {
                    otherControl.DAMAGE_DATA.AttackerIsLeft = true;
                }
                else
                {
                    otherControl.DAMAGE_DATA.AttackerIsRight = true;
                }
                otherControl.DAMAGE_DATA.TakeDamage(ProjectileDamage);
            }
        }
    }
}