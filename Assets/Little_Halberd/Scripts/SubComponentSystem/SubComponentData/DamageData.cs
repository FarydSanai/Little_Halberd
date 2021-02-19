using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class DamageData
    {
        public float CurrentHP;
        public bool isDead;

        public delegate void CharacterAction(float damage);
        public CharacterAction TakeDamage;
    }
}