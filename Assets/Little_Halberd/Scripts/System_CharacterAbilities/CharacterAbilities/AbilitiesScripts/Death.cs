using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    [CreateAssetMenu(fileName = "New ability", menuName = "LittleHalberd/Ability/Death")]
    public class Death : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.control.DAMAGE_DATA.ProcessDeath(characterState.control);
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
