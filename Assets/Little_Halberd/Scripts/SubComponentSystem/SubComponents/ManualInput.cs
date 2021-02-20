using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class ManualInput : SubComponent
    {
        private void Start()
        {
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.MANUAL_INPUT] = this;
        }
        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {
            if (VirtualInputManager.Instance.MoveLeft)
            {
                control.MoveLeft = true;
            }
            else
            {
                control.MoveLeft = false;
            }

            if (VirtualInputManager.Instance.MoveRight)
            {
                control.MoveRight = true;
            }
            else
            {
                control.MoveRight = false;
            }

            if (VirtualInputManager.Instance.Run)
            {
                control.Run = true;
            }
            else
            {
                control.Run = false;
            }

            if (VirtualInputManager.Instance.Jump)
            {
                control.Jump = true;
            }
            else
            {
                control.Jump = false;
            }

            if (VirtualInputManager.Instance.Attack)
            {
                control.Attack = true;
            }
            else
            {
                control.Attack = false;
            }
        }
    }
}