using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class PlayerInput : MonoBehaviour
    {
        private void Update()
        {
            VirtualInputManager.Instance.MoveLeft =
                Input.GetKey(VirtualInputManager.Instance.InputKeysDic[InputKeyType.KEY_MOVE_LEFT]);

            VirtualInputManager.Instance.MoveRight =
                Input.GetKey(VirtualInputManager.Instance.InputKeysDic[InputKeyType.KEY_MOVE_RIGHT]);

            VirtualInputManager.Instance.Run =
                Input.GetKey(VirtualInputManager.Instance.InputKeysDic[InputKeyType.KEY_RUN]);

            VirtualInputManager.Instance.Jump =
                Input.GetKey(VirtualInputManager.Instance.InputKeysDic[InputKeyType.KEY_JUMP]);

            VirtualInputManager.Instance.Attack =
                Input.GetKeyDown(VirtualInputManager.Instance.InputKeysDic[InputKeyType.KEY_ATTACK]);
        }
    }
}