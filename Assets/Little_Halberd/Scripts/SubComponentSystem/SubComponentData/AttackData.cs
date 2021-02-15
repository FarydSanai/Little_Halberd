using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class AttackData
    {
        public float AttackDamage;
        public float AttackRange;

        public Transform AttackPoint;
        public LayerMask EnemyLayers;

        public delegate void CharacterAction();
        public CharacterAction Attack;
    }
}
