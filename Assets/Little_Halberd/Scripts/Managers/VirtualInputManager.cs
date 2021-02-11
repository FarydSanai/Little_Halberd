using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public enum InputKeyType
    {
        KEY_MOVE_LEFT,
        KEY_MOVE_RIGHT,
        KEY_RUN,
        KEY_JUMP,
        KEY_ATTACK,
    }
    public class VirtualInputManager : Singleton<VirtualInputManager>
    {
        public bool MoveLeft;
        public bool MoveRight;
        public bool Run;
        public bool Jump;
        public bool Attack;

        public Dictionary<InputKeyType, KeyCode> InputKeysDic = new Dictionary<InputKeyType, KeyCode>();

        private void Awake()
        {
            InputKeysDic.Add(InputKeyType.KEY_MOVE_LEFT,    KeyCode.A);
            InputKeysDic.Add(InputKeyType.KEY_MOVE_RIGHT,   KeyCode.D);
            InputKeysDic.Add(InputKeyType.KEY_RUN,          KeyCode.LeftShift);
            InputKeysDic.Add(InputKeyType.KEY_JUMP,         KeyCode.Space);
            InputKeysDic.Add(InputKeyType.KEY_ATTACK,       KeyCode.Return);

            GameObject obj = Instantiate(Resources.Load("PlayerInput", typeof(GameObject))) as GameObject;
        }
    }
}