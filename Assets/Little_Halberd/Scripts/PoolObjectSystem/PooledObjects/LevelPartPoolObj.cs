using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Foliage;

namespace LittleHalberd
{
    public class LevelPartPoolObj : MonoBehaviour, IPooledObject
    {
        public ObjectType objectType => Type;

        [SerializeField]
        private ObjectType Type;
        [SerializeField]
        private bool HasFoliagePath;

        private void Start()
        {
            AstarPath.active.Scan();

            //For dev only
            if (HasFoliagePath)
            {
                FoliageMeshHelper.EnableMeshForGrassPath(this);
            }
            //-----------
        }
    }
}