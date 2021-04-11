using UnityEngine;
using Foliage;

namespace LittleHalberd
{
    public static class FoliageMeshHelper
    {
        public static void EnableMeshForGrassPath(MonoBehaviour monoBeh)
        {
            Foliage2D_Path[] FoliagePathArr = monoBeh.gameObject.GetComponentsInChildren<Foliage2D_Path>();
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