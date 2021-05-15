using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class DamageDetector : SubComponent
    {
        public DamageData damageData;
        private Coroutine DeathRoutine;

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
                ProcessDeath = ProcessDeath,
            };
            subComponentProcessor.damageData = damageData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.DAMAGE_DETECTOR] = this;

            if (DeathRoutine != null)
            {
                StopCoroutine(DeathRoutine);
            }
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
                damageData.isDead = true;
                control.characterAnimator.SetBool(HashManager.Instance.ArrTransitionParams[(int)TransitionParameter.IsDead], true);
                //ProcessDeath();
            }
        }
        private void ProcessDeath(CharacterControl control)
        {
            if (DeathRoutine == null)
            {
                DeathRoutine = StartCoroutine(_DeathFadeOut(control));
            }
        }

        private IEnumerator _DeathFadeOut(CharacterControl control)
        {
            float opacity = 1;
            SpriteRenderer[] sprites = control.gameObject.GetComponentsInChildren<SpriteRenderer>();
            while (opacity > 0f)
            {
                opacity -= 0.1f;

                for (int i = 0; i < sprites.Length; i++)
                {
                    if (opacity < 0f)
                    {
                        opacity = 0f;
                    }
                    Color c = sprites[i].material.color;
                    c.a = opacity;
                    yield return new WaitForEndOfFrame();
                    sprites[i].color = c;
                }
                
                yield return new WaitForSeconds(0.1f);

            }
            control.gameObject.SetActive(false);
            yield return new WaitForEndOfFrame();
            if (control.gameObject.layer == CustomLayers.Instance.GetLayer(LH_Layer.Player))
            {
                control.gameObject.SetActive(false);
            }
            else
            {
                PoolObjectLoader.Instance.DestroyObject(control.gameObject);
            }
            StopCoroutine(DeathRoutine);
            //yield return new WaitForEndOfFrame();
        }
    }
}