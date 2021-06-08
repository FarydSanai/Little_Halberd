using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public abstract class SubComponent : MonoBehaviour
    {
        protected SubComponentProcessor subComponentProcessor;
        public CharacterControl control
        {
            get
            {
                return subComponentProcessor.control;
            }
        }
        private void Awake()
        {
            subComponentProcessor = this.gameObject.GetComponentInParent<SubComponentProcessor>();
        }
        public abstract void OnUpdate();
        public abstract void OnFixedUpdate();
    }
}