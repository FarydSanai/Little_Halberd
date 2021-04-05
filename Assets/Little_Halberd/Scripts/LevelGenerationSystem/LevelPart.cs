using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Foliage;

namespace LittleHalberd
{
    public class LevelPart : MonoBehaviour, IPooledObject
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
                EnableMeshForGrassPath();
            }
            //-----------
        }
        private void EnableMeshForGrassPath()
        {
            Foliage2D_Path[] FoliagePathArr = this.GetComponentsInChildren<Foliage2D_Path>();
            for (int i = 0; i < FoliagePathArr.Length; i++)
            {
                Foliage2D[] fol2dArr = FoliagePathArr[i].GetComponentsInChildren<Foliage2D>();
                for (int j = 0; j < fol2dArr.Length; j++)
                {
                    fol2dArr[j].RebuildMesh();
                }
            }
        }
    }
}