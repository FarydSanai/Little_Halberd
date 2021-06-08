using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class CharacterMovement : SubComponent
    {
        public MovingData movingData;
        public List<ContactPoint2D> contacts = new List<ContactPoint2D>();

        private Coroutine CheckYPosRoutine;
        private void Start()
        {
            movingData = new MovingData
            {
                MovementSpeed = 0f,
                MovementSpeedGraph = null,
                StateNormalizedTime = 0f,
                UncontrolledMoving = false,
            };

            subComponentProcessor.movingData = movingData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.CHARACTER_MOVEMENT] = this;

            if (CheckYPosRoutine != null)
            {
                StopCoroutine(CheckYPosRoutine);
            }
            else
            {
                CheckYPosRoutine = StartCoroutine(_CheckYPos());
            }
        }
        public override void OnFixedUpdate()
        {
            if (control.characterAnimator.GetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Move]))
            {
                if (movingData.UncontrolledMoving)
                {
                    Move(movingData.MovementSpeed, movingData.MovementSpeedGraph, movingData.StateNormalizedTime);
                }
                else
                {
                    if (control.MoveLeft)
                    {
                        Move(-movingData.MovementSpeed, movingData.MovementSpeedGraph, movingData.StateNormalizedTime);
                    }
                    if (control.MoveRight)
                    {
                        Move(movingData.MovementSpeed, movingData.MovementSpeedGraph, movingData.StateNormalizedTime);
                    }
                }
            }
            else
            {
                if (subComponentProcessor.groundData.IsGrounded())
                {
                    control.RIGID_BODY.velocity = new Vector2(0f, control.RIGID_BODY.velocity.y);
                }
            }
        }
        public override void OnUpdate()
        {
            //throw new System.NotImplementedException();
        }
        private void Move(float speed, AnimationCurve speedGraph, float stateNormalizedTime)
        {
            control.RIGID_BODY.velocity = new Vector2(speed * speedGraph.Evaluate(stateNormalizedTime),
                                                      control.RIGID_BODY.velocity.y);
            if (!movingData.UncontrolledMoving)
            {
                SetDirection(speed);
            }
        }
        private void SetDirection(float dir)
        {
            if (dir == 0)
            {
                return;
            }
            if (dir < 0)
            {
                control.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else
            {
                control.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
        private IEnumerator _CheckYPos()
        {
            while (true)
            {
                if (control.transform.position.y < -20f)
                {
                    control.gameObject.SetActive(false);
                }
                yield return new WaitForSeconds(5f);
            }
        }
    }
}