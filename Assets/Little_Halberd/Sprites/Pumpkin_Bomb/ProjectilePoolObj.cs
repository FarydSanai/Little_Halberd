﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class ProjectilePoolObj : MonoBehaviour, IPooledObject
    {
        public ObjectType objectType => Type;

        [SerializeField]
        private ObjectType Type;
    }
}