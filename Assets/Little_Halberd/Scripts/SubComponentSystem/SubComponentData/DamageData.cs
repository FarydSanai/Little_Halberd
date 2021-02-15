using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class DamageData
    {
        //public CharacterControl attacker;
        public float CurrentHP;
        public bool isDead;

        public delegate void CharacterAction(float damage);
        public CharacterAction TakeDamage;
    }
}