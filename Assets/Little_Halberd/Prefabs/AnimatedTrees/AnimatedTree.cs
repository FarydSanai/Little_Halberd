using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Foliage;

namespace LittleHalberd
{
    public class AnimatedTree : MonoBehaviour
    {
        void OnEnable()
        {
            this.GetComponent<Foliage2D>().RebuildMesh();
        }
    }
}