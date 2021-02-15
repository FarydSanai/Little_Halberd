using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    [CreateAssetMenu(fileName = "New ability", menuName = "LittleHalberd/Ability/Attack")]
    public class Attack : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //if (characterState.control.Attack)
            //{
            //    animator.SetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Attack], true);
            //}
            //else
            //{
            //    animator.SetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Attack], false);
            //}
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
    }
}


