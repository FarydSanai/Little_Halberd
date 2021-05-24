using System;
using UnityEngine;

namespace LittleHalberd
{
    public class CharacterState : StateMachineBehaviour
    {
        [HideInInspector] public CharacterControl control;
        public MovingData MOVING_DATA => control.subComponentProcessor.movingData;
        public JumpData JUMP_DATA => control.subComponentProcessor.jumpData;
        public GroundData GROUND_DATA => control.subComponentProcessor.groundData;
        public DamageData DAMAGE_DATA => control.subComponentProcessor.damageData;
        public AttackData ATTACK_DATA => control.subComponentProcessor.attackData;
        public RangeAttackData RANGE_ATTACK_DATA => control.subComponentProcessor.rangeAttackData;

        [Space(3f)]
        public CharacterAbility[] CharacterAbilityArr;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (control == null)
            {
                control = animator.transform.root.GetComponent<CharacterControl>();
                control.InitCharacterStates(animator);
            }

            for (int i = 0; i < CharacterAbilityArr.Length; i++)
            {
                CharacterAbilityArr[i].OnEnter(this, animator, stateInfo);
            }
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            for (int i = 0; i < CharacterAbilityArr.Length; i++)
            {
                CharacterAbilityArr[i].UpdateAbility(this, animator, stateInfo);
            }
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            for (int i = 0; i < CharacterAbilityArr.Length; i++)
            {
                CharacterAbilityArr[i].OnExit(this, animator, stateInfo);
            }
        }
    }
}