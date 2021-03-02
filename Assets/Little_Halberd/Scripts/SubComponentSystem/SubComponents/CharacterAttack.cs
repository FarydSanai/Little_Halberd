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
        public float AttackResetTime = 0f;

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
                AttackIsReset = true,
                AttackTimer = 0f,
                Attack = Attack,


            };
            subComponentProcessor.attackData = attackData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.CHARACTER_ATTACK] = this;
        }

        public override void OnUpdate()
        {
            ProcessAttack();
        }
        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }
        private void ProcessAttack()
        {
            if (control.Attack)
            {
                if (attackData.AttackIsReset)
                {
                    control.characterAnimator.SetTrigger(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Attack]);
                    attackData.Attack();

                    if (AttackResetTime > 0f)
                    {
                        attackData.AttackTimer = Time.time + AttackResetTime;
                        attackData.AttackIsReset = false;
                    }
                }
                else
                {
                    if (Time.time > attackData.AttackTimer)
                    {
                        attackData.AttackIsReset = true;
                    }
                }
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