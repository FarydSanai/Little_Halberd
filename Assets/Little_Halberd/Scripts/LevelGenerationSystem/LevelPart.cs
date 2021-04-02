using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class LevelPart : MonoBehaviour, IPooledObject
    {
        public ObjectType objectType => Type;

        [SerializeField]
        private ObjectType Type;

        private void Start()
        {
            AstarPath.active.Scan();
        }
    }
}