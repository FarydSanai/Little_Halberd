using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    [CreateAssetMenu(fileName = "New ability", menuName = "LittleHalberd/Ability/Attack")]
    public class Attack : CharacterAbility
    {
        [SerializeField] private LayerMask ProjectileLayer;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.AI_DATA.isRage)
            {
                characterState.ATTACK_DATA.AttackDamage = 100;
            }
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.ATTACK_DATA.RepelPoint != null)
            {
                RepelProjectile(characterState, stateInfo);
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
        private void RepelProjectile(CharacterState characterState, AnimatorStateInfo stateInfo)
        {
            Collider2D[] projectiles =
                               Physics2D.OverlapCircleAll(characterState.ATTACK_DATA.RepelPoint.position,
                                                          2f,
                                                          ProjectileLayer);
            if (projectiles.Length > 0)
            {
                Projectile projectile = projectiles[0].gameObject.GetComponent<Projectile>();
                if (stateInfo.normalizedTime >= 0.4f &&
                    stateInfo.normalizedTime <= 0.9f)
                {
                    PoolObjectLoader.Instance.GetObject(ObjectType.VFX_BOMB_REPEL,
                                                        projectile.gameObject.transform.position,
                                                        Quaternion.identity);
                    PoolObjectLoader.Instance.DestroyObject(projectile.gameObject);
                }
            }
        }
    }
}


