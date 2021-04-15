using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class GroundPoolObj : MonoBehaviour, IPooledObject
    {
        public ObjectType objectType => Type;

        [SerializeField]
        private ObjectType Type;
        [SerializeField]
        private bool HasFoliagePath;

        private void Start()
        {
            if (HasFoliagePath)
            {
                FoliageMeshHelper.EnableMeshForGrassPath(this);
            }
        }
    }
}