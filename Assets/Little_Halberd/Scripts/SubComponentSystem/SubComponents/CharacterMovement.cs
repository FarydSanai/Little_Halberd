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
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.CHARACTER_MOVEMENT] = this;
        }
        public override void OnFixedUpdate()
        {
            if (control.characterAnimator.GetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Move]))
            {
                if (control.MoveLeft)
                {
                    if (!CheckScreenLeftBound())
                    {
                        control.RIGID_BODY.velocity = new Vector2(0f, control.RIGID_BODY.velocity.y);
                        return;
                    }
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
        private bool CheckScreenLeftBound()
        {
            if (CharacterManager.Instance.CharacterIsPlayable(control))
            {
                Vector3 pos = Camera.main.WorldToScreenPoint(control.transform.position);
                //Debug.Log(pos);
                if (pos.x <= 70f)
                {
                    movingData.MovementSpeed = 0f;
                    return false;
                }
                return true;
            }
            return true;
        }
    }
}