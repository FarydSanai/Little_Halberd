﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public enum LH_Layer
    {
        Ground,
        Enemy,
        Player,
        PostProcessing,
        RopeBlock,
        Projectile,
    }
    public class CustomLayers : Singleton<CustomLayers>
    {
        public Dictionary<string, int> LayersDic = new Dictionary<string, int>();
        private void Awake()
        {
            LayersDic = GetAllLAyers();
        }
        private Dictionary<string, int> GetAllLAyers()
        {
            Dictionary<string, int> AllLayers = new Dictionary<string, int>();
            LH_Layer[] layers = System.Enum.GetValues(typeof(LH_Layer)) as LH_Layer[];
            for (int i = 0; i < layers.Length; i++)
            {
                AllLayers[layers[i].ToString()] = LayerMask.NameToLayer(layers[i].ToString());
            }
            return AllLayers;
        }
        public int GetLayer(LH_Layer layer)
        {
            return LayersDic[layer.ToString()];
        }
    }
}


