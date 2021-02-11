using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class CharacterMovement : SubComponent
    {
        public MovingData movingData;
        public List<ContactPoint2D> contacts = new List<ContactPoint2D>();
        private void Start()
        {
            movingData = new MovingData
            {
                MovementSpeed = 0f,
                MovementSpeedGraph = null,
                StateNormalizedTime = 0f,
            };

            subComponentProcessor.movingData = movingData;
            subComponentProcessor.ArrSubComponentns[(int)SubComponentType.CHARACTER_MOVEMENT] = this;
        }
        public override void OnFixedUpdate()
        {
            if (control.characterAnimator.GetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Move]))
            {
                if (HasContactInAir())
                {
                    return;
                }
                if (control.MoveLeft)
                {
                    Move(-movingData.MovementSpeed, movingData.MovementSpeedGraph, movingData.StateNormalizedTime);
                }
                if (control.MoveRight)
                {
                    Move(movingData.MovementSpeed, movingData.MovementSpeedGraph, movingData.StateNormalizedTime);
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
            throw new System.NotImplementedException();
        }
        private void Move(float speed, AnimationCurve speedGraph, float stateNormalizedTime)
        {
            control.RIGID_BODY.velocity = new Vector2(speed * speedGraph.Evaluate(stateNormalizedTime),
                                                      control.RIGID_BODY.velocity.y);
            SetDirection(speed);
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
        private bool HasContactInAir()
        {
            if (!subComponentProcessor.groundData.IsGrounded())
            {
                int cotactsList = control.RIGID_BODY.GetContacts(contacts);
                if (cotactsList > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}