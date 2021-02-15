using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class CharacterAttack : SubComponent
    {
        public AttackData attackData;

        public float AttackRange = 0.5f;
        public float AttackDamage = 30;

        public Transform AttackPoint;
        public LayerMask EnemyLayers;

        private void Start()
        {
            attackData = new AttackData
            {
                AttackDamage = AttackDamage,
                AttackRange = AttackRange,
                AttackPoint = AttackPoint,
                EnemyLayers = EnemyLayers,
                Attack = Attack,
            };
            subComponentProcessor.attackData = attackData;
            subComponentProcessor.ArrSubComponentns[(int)SubComponentType.CHARACTER_ATTACK] = this;
        }

        public override void OnUpdate()
        {
            if (control.Attack)
            {
                control.characterAnimator.SetTrigger(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Attack]);
                attackData.Attack();
            }
        }
        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
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
                enemyCol.GetComponent<CharacterControl>().DAMAGE_DATA.TakeDamage(attackData.AttackDamage);
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