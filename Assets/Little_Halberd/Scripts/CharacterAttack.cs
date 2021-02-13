using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class CharacterAttack : MonoBehaviour
    {
        private CharacterControl control;

        public float AttackRange = 0.5f;
        public float AttackDamage = 30;

        public Transform AttackPoint;
        public LayerMask EnemyLayers;
        private void Awake()
        {
            control = this.GetComponentInChildren<CharacterControl>(); 
        }
        private void Update()
        {
            if (control.Attack)
            {
                control.characterAnimator.SetTrigger("Att");
                Attack();
            }
        }
        private void Attack()
        {    
            Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);
            foreach (Collider2D enemyCol in enemyColliders)
            {
                if (enemyCol.GetType() == typeof(CapsuleCollider2D))
                {
                    continue;
                }
                enemyCol.GetComponent<EnemyControl>().GetDamage(AttackDamage);
                //Debug.Log("Hit : " + enemyCol.name);
            }
        }
        private void OnDrawGizmosSelected()
        {
            if (AttackPoint == null)
            {
                return;
            }
            Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
        }
    }
}