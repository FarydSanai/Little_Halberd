using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    [System.Serializable]
    public class MovingData
    {
        public float MovementSpeed;
        public AnimationCurve MovementSpeedGraph;
        public float StateNormalizedTime;
    }
}
