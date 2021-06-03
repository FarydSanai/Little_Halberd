using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    [CreateAssetMenu(fileName = "New ability", menuName = "LittleHalberd/Ability/Jump")]
    public class Jump : CharacterAbility
    {
        public float JumpForce;
        public bool JumpCancel;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Jump], true);

            characterState.JUMP_DATA.JumpCancel = JumpCancel;
            characterState.JUMP_DATA.JumpForce = JumpForce;

            PoolObjectLoader.Instance.GetObject(ObjectType.SFX_JUMP);
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!characterState.control.Jump)
            {
                animator.SetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Jump], false);
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Jump], false);
            characterState.JUMP_DATA.Jumped = false;
        }
    }
}