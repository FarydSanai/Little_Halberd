﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class DamageDetector : SubComponent
    {
        public DamageData damageData;

        private void Start()
        {
            damageData = new DamageData
            {
                CurrentHP = control.CharacterMaxHP,
                isDead = false,
                TakeDamage = TakeDamage,
                AttackerIsLeft = false,
                AttackerIsRight = false,
                isDamaging = false,
            };

            subComponentProcessor.damageData = damageData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.DAMAGE_DETECTOR] = this;
        }

        public override void OnUpdate()
        {
            //throw new System.NotImplementedException();
        }

        public override void OnFixedUpdate()
        {
            //throw new System.NotImplementedException();
        }
        public void TakeDamage(float damage)
        {
            damageData.CurrentHP -= damage;
            subComponentProcessor.healthBarData.ChangeHealthBar(damage);
            control.characterAnimator.SetTrigger(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.Damaged]);

            if(damageData.CurrentHP <= 0f)
            {
                ProcessDeath();
            }
        }
        private void ProcessDeath()
        {
            damageData.isDead = true;
            control.gameObject.SetActive(false);
        }
    }
}