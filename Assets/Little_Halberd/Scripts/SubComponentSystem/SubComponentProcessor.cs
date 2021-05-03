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
        RANGE_ATTACK,

        COUNT,
    }
    public class SubComponentProcessor : MonoBehaviour
    {
        public SubComponent[] ArrSubComponents;

        [HideInInspector] public CharacterControl control;

        public MovingData movingData;
        public JumpData jumpData;
        public GroundData groundData;
        public AttackData attackData;
        public DamageData damageData;
        public RangeAttackData rangeAttackData;

        private void Awake()
        {
            ArrSubComponents = new SubComponent[(int)SubComponentType.COUNT];
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
            UpdateSubComponent(SubComponentType.RANGE_ATTACK);
        }

        private void FixedUpdateSubComponent(SubComponentType type)
        {
            if (ArrSubComponents[(int)type] != null)
            {
                ArrSubComponents[(int)type].OnFixedUpdate();
            }
        }
        private void UpdateSubComponent(SubComponentType type)
        {
            if (ArrSubComponents[(int)type] != null)
            {
                ArrSubComponents[(int)type].OnUpdate();
            }
        }
    }
}