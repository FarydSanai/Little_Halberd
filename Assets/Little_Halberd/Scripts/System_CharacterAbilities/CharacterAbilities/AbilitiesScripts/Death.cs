using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LittleHalberd
{
    [CreateAssetMenu(fileName = "New ability", menuName = "LittleHalberd/Ability/Death")]
    public class Death : CharacterAbility
    {
        private float opacity;
        private SpriteRenderer[] sprites;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.control.CHARACTER_AI_DATA.FollowEnabled = false;
            sprites = characterState.control.gameObject.
                                GetComponentsInChildren<SpriteRenderer>();
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            opacity = 1 - stateInfo.normalizedTime;
            if (opacity <= 0.01f)
            {
                DestroyCharacter(characterState.control.gameObject);
                return;
            }
            DecreaseSpriteOpacity(sprites);
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
        private void DecreaseSpriteOpacity(SpriteRenderer[] sprites)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                if (opacity < 0f)
                {
                    opacity = 0f;
                }
                Color c = sprites[i].material.color;
                c.a = opacity;
                sprites[i].color = c;
            }
        }
        private void DestroyCharacter(GameObject obj)
        {
            IPooledObject pooledObj = obj.GetComponent<IPooledObject>();
            if (pooledObj != null)
            {
                if (PoolObjectLoader.Instance.EnemyTypeInfo.Any(e => e.objectType ==
                                                                     pooledObj.objectType))
                {
                    PoolObjectLoader.Instance.DestroyObject(obj);
                }
                else
                {
                    Destroy(obj);
                }
            }
            else
            {
                Destroy(obj);
            }
        }
    }
}
