using System.Collections;
using System;
using UnityEngine;

namespace LittleHalberd
{
    public enum SubComponentType
    {
        MANUAL_INPUT,
        CHARACTER_MOVEMENT,
        CHARACTER_JUMP,
        CHARACTER_GROUND,
        CHARACTER_ATTACK,
        DAMAGE_DETECTOR,

        COUNT,
    }
    public class SubComponentProcessor : MonoBehaviour
    {
        public SubComponent[] ArrSubComponentns;

        [NonSerialized] public CharacterControl control;

        public MovingData movingData;
        public JumpData jumpData;
        public GroundData groundData;
        public AttackData attackData;
        public DamageData damageData;

        private void Awake()
        {
            ArrSubComponentns = new SubComponent[(int)SubComponentType.COUNT];
            control = this.GetComponentInParent<CharacterControl>();
        }


        public void FixedUpdateSubComponents()
        {
            FixedUpdateSubComponent(SubComponentType.CHARACTER_MOVEMENT);
            FixedUpdateSubComponent(SubComponentType.CHARACTER_JUMP);
        }
        public void UpdateSubComponents()
        {
            UpdateSubComponent(SubComponentType.MANUAL_INPUT);
            UpdateSubComponent(SubComponentType.CHARACTER_ATTACK);
        }

        private void FixedUpdateSubComponent(SubComponentType type)
        {
            if (ArrSubComponentns[(int)type] != null)
            {
                ArrSubComponentns[(int)type].OnFixedUpdate();
            }
        }
        private void UpdateSubComponent(SubComponentType type)
        {
            if (ArrSubComponentns[(int)type] != null)
            {
                ArrSubComponentns[(int)type].OnUpdate();
            }
        }
    }
}