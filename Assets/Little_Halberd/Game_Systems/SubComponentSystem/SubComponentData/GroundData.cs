using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    [System.Serializable]
    public class GroundData
    {
        public delegate bool GetBool();

        public GetBool IsGrounded;
    }
}