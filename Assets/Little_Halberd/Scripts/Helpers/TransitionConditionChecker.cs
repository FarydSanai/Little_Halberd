using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public static class TransitionConditionChecker 
    {
        public static bool MakeTransition(CharacterControl control, List<TransitionConditionType> transitions)
        {
            foreach (TransitionConditionType c in transitions)
            {
                switch (c)
                {
                    case TransitionConditionType.MoveForward:
                        {
                            if (!control.MoveLeft && !control.MoveRight)
                            {
                                return false;
                            }
                            if (control.MoveLeft && control.MoveRight)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.Jump:
                        {
                            if (!control.Jump)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.Attack:
                        {
                            if (!control.Attack)
                            {
                                return false;
                            }
                        }
                        break;
                    case TransitionConditionType.NotMoving:
                        {
                            if (control.MoveLeft || control.MoveRight)
                            {
                                if (!(control.MoveLeft && control.MoveRight))
                                {
                                    return false;
                                }
                            }
                        }
                        break;
                    case TransitionConditionType.Run:
                        {
                            if (!control.Run)
                            {
                                return false;
                            }
                        }
                        break;
                }
            }
            return true;
        }
    }
}