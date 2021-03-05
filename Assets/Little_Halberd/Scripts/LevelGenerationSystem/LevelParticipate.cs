using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class LevelParticipate : MonoBehaviour, IPooledObject
    {
        public ObjectType objectType => Type;

        [SerializeField]
        private ObjectType Type;

    }
}