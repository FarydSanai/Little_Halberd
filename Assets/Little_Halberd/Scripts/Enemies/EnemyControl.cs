using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class EnemyControl : MonoBehaviour
    {
        public Animator EnemyAnimator;
        public float MaxHP = 100;
        private float CurrentHP;
        public bool isDead;

        private void Awake()
        {
            EnemyAnimator = this.GetComponentInChildren<Animator>();
        }
        private void Start()
        {
            CurrentHP = MaxHP;
        }
        public void GetDamage(float damage)
        {
            CurrentHP -= damage;
            EnemyAnimator.SetTrigger("Damaged");

            if(CurrentHP <= 0f)
            {
                isDead = true;
                EnemyAnimator.SetBool("isDead", true);
            }
        }
    }
}