using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    [CreateAssetMenu(fileName = "New ability", menuName = "LittleHalberd/Ability/TakeDamage")]
    public class TakeDamage : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.DAMAGE_DATA.isDamaging = true;
            PoolObjectLoader.Instance.GetObject(ObjectType.SFX_IMPACT,
                                                characterState.control.transform.position,
                                                Quaternion.identity);
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.DAMAGE_DATA.isDead)
            {
                animator.SetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.IsDead], true);
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.DAMAGE_DATA.isDamaging = false;
            characterState.DAMAGE_DATA.AttackerIsRight = false;
            characterState.DAMAGE_DATA.AttackerIsLeft = false;
        }
    }
}