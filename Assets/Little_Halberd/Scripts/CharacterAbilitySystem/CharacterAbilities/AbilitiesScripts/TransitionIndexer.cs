using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public enum TransitionConditionType
    {
        MoveForward,
        Jump,
        Attack,
        NotMoving,
        Run,
    }
    [CreateAssetMenu(fileName = "New ability", menuName = "LittleHalberd/Ability/TransitionIndexer")]
    public class TransitionIndexer : CharacterAbility
    {
        public int Index;
        public List<TransitionConditionType> TransitionConditions = new List<TransitionConditionType>();

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (TransitionConditionChecker.MakeTransition(characterState.control, TransitionConditions))
            {
                animator.SetInteger(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.TransitionIndex], Index);
            }
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (animator.GetInteger(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.TransitionIndex]) == 0)
            {
                if (TransitionConditionChecker.MakeTransition(characterState.control, TransitionConditions))
                {
                    animator.SetInteger(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.TransitionIndex], Index);
                }
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetInteger(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.TransitionIndex], 0);
        }
    }
}