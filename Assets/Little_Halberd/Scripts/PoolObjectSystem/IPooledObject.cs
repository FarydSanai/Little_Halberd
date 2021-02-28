using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    interface IPooledObject
    {
        ObjectType objectType { get; }
    }
}