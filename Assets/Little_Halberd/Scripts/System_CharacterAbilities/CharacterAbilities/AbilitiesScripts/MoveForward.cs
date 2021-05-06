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

        public bool UncontrolledMoving;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.MOVING_DATA.MovementSpeedGraph = SpeedGraph;
            characterState.MOVING_DATA.UncontrolledMoving = UncontrolledMoving;

            characterState.MOVING_DATA.MovementSpeed = Speed;
            if (characterState.DAMAGE_DATA.isDamaging)
            {
                SetDamageImpulse(characterState);
            }
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (UncontrolledMoving)
            {
                animator.SetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Move], true);
            }
            else
            {
                ControlledMoving(characterState, animator, stateInfo);
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Run], false);
            animator.SetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Move], false);
            characterState.MOVING_DATA.UncontrolledMoving = false;
        }
        private void SetDamageImpulse(CharacterState characterState)
        {
            if (characterState.DAMAGE_DATA.AttackerIsLeft)
            {
                characterState.MOVING_DATA.MovementSpeed *= 1f;
            }
            if (characterState.DAMAGE_DATA.AttackerIsRight)
            {
                characterState.MOVING_DATA.MovementSpeed *= -1f;
            }
        }
        private void ControlledMoving(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
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

                CheckRun(animator, characterState.control.Run);

                characterState.MOVING_DATA.StateNormalizedTime = stateInfo.normalizedTime;
            }
        }
        private void CheckRun(Animator animator, bool run)
        {
            animator.SetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Run], run);
        }
    }
}
