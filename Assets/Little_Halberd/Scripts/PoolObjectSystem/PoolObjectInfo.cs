using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    [System.Serializable]
    public struct PoolObjectInfo
    {
        public ObjectType objectType;
        public GameObject objectPrefab;
        public int StartCount;
    }
}