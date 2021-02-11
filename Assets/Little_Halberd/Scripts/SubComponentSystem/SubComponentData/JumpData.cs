using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    [System.Serializable]
    public class JumpData
    {
        public bool Jumped;
        public bool JumpCancel;

        public float JumpForce;

        public delegate void CharacterAction(float force);
        public CharacterAction CharacterJump;
    }
}