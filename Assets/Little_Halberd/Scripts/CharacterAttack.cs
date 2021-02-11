using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class CharacterAttack : MonoBehaviour
    {
        private CharacterControl control;

        private void Awake()
        {
            control = this.GetComponentInChildren<CharacterControl>(); 
        }
        private void Update()
        {
            if (control.Attack)
            {
                control.characterAnimator.SetTrigger("Att");
            }
        }
    }
}