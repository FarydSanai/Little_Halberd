using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    [CreateAssetMenu(fileName = "New ability", menuName = "LittleHalberd/Ability/MoveForward")]
    public class MoveForward : CharacterAbility
    {
        public float Speed;
        public AnimationCurve SpeedGraph;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.MOVING_DATA.MovementSpeed = Speed;
            characterState.MOVING_DATA.MovementSpeedGraph = SpeedGraph;
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!characterState.control.MoveLeft && !characterState.control.MoveRight)
            {
                animator.SetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Move], false);
                return;
            }
            if (characterState.control.MoveLeft && characterState.control.MoveRight)
            {
                animator.SetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Move], false);
                return;
            }
            if (characterState.control.MoveRight || characterState.control.MoveLeft)
            {
                animator.SetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Move], true);

                if (characterState.control.Run)
                {
                    animator.SetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Run], true);
                }
                else
                {
                    animator.SetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Run], false);
                }

                characterState.MOVING_DATA.StateNormalizedTime = stateInfo.normalizedTime;

            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Run], false);
            animator.SetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Move], false);
        }
    }
}
