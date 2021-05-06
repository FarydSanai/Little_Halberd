using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    [CreateAssetMenu(fileName = "New ability", menuName = "LittleHalberd/Ability/GroundDetector")]
    public class GroundDetector : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.GROUND_DATA.IsGrounded())
            {
                animator.SetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Grounded], true);
            }
            else
            {
                animator.SetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Grounded], false);
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
    }
}