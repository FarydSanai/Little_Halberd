﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    [CreateAssetMenu(fileName = "New ability", menuName = "LittleHalberd/Ability/RangeAttackAbility")]
    public class RangeAttackAbility : CharacterAbility
    {
        [Header("Data Settings")]
        public float AngleInDegrees;
        public GameObject ProjectilePrefab;
        [Header("Time settings")]
        public float StartTimeBtwAttacks;

        private bool IsReset;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            InitRangeAttackData(characterState);
            IsReset = true;
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (IsReset)
            {
                characterState.RANGE_ATTACK_DATA.ProcessRangeAtatck();
                IsReset = false;
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
        private void InitRangeAttackData(CharacterState characterState)
        {
            characterState.RANGE_ATTACK_DATA.Target = CharacterManager.Instance.PlayableCharacter.transform;
            characterState.RANGE_ATTACK_DATA.SpawnPoint = characterState.ATTACK_DATA.AttackPoint;
            characterState.RANGE_ATTACK_DATA.AngleInDegrees = AngleInDegrees;
            characterState.RANGE_ATTACK_DATA.ProjectilePrefab = ProjectilePrefab;
            characterState.RANGE_ATTACK_DATA.StartTimeBtwAttacks = StartTimeBtwAttacks;
        }
    }
}
