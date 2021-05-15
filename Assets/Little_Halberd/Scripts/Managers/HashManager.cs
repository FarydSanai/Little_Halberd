using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public enum TransitionParameter
    {
        Move,
        Run,
        Jump,
        Attack,
        TransitionIndex,
        Grounded,
        LockTransition,
        IsDead,
        Damaged,
        RangeAttack,

        COUNT,
    }
    public class HashManager : Singleton<HashManager>
    {
        public int[] ArrTransitionParams = new int[(int)TransitionParameter.COUNT];
        private void Awake()
        {
            //Temp
            Debug.Log(CustomLayers.Instance.name);

            //Parameters for animation transitions
            for (int i = 0; i < (int)TransitionParameter.COUNT; i++)
            {
                ArrTransitionParams[i] = Animator.StringToHash(((TransitionParameter)i).ToString());
            }
        }
    }
}