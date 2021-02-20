using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class CharacterJump : SubComponent
    {
        public JumpData jumpData;

        public void Start()
        {
            jumpData = new JumpData
            {
                Jumped = false,
                JumpCancel = false,
                JumpForce = 0f,
                CharacterJump = CharJump,
            };
            subComponentProcessor.jumpData = jumpData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.CHARACTER_JUMP] = this;
        }
        
        public override void OnFixedUpdate()
        {
            if (control.characterAnimator.GetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Jump]) == true)
            {
                if (control.Jump)
                {
                    if (!jumpData.Jumped)
                    {
                        jumpData.CharacterJump(jumpData.JumpForce);
                        jumpData.Jumped = true;
                    }
                }
            }
            if (jumpData.JumpCancel)
            {
                if (control.RIGID_BODY.velocity.y > 0f && !control.Jump)
                {
                    control.RIGID_BODY.velocity -= (Vector2.up * control.RIGID_BODY.velocity.y * 0.1f);
                }

            }
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        private void CharJump(float force)
        {
            control.RIGID_BODY.velocity = (new Vector2(control.RIGID_BODY.velocity.x * 0.05f, 1f) * force);
            //control.RIGID_BODY.AddForce(new Vector2(control.RIGID_BODY.velocity.x * 0.05f, 1f) * force);
        }

    }
}