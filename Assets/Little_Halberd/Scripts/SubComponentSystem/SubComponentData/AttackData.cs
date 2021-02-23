using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    [System.Serializable]
    public class AttackData
    {
        public float AttackDamage;
        public float AttackRange;
        public float AttackTimer;

        public bool AttackTriggered;
        public bool AttackIsReset;

        public Transform AttackPoint;
        public LayerMask EnemyLayers;

        public delegate void CharacterAction();
        public CharacterAction Attack;
    }
}
