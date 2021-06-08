using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    [System.Serializable]
    public class CharacterAIData
    {
        public float NextPointDistance;
        public float JumpNodeRequireDist;

        public EnemyType EnemyType;
        public AIState InitialState;
        public AIState AICurrentState;
        public bool FollowEnabled;
        public bool isRage;

        public CharacterControl targetControl;

        public delegate void AIBehaviour();
        public AIBehaviour ProcessAIBehaviour;

    }
}
