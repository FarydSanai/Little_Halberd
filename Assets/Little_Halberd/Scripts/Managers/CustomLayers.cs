using System;
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
            //int my = 0;

            Dictionary<string, int> AllLayers = new Dictionary<string, int>();
            LH_Layer[] layers = System.Enum.GetValues(typeof(LH_Layer)) as LH_Layer[];
            for (int i = 0; i < layers.Length; i++)
            {
                AllLayers[layers[i].ToString()] = LayerMask.NameToLayer(layers[i].ToString());
                //my = AllLayers[layers[i].ToString()];
                //Debug.Log(Convert.ToString(my, 2).PadLeft(32, '0'));
            }
            return AllLayers;
        }

        public int GetLayer(LH_Layer layer)
        {
            return LayersDic[layer.ToString()];
        }
    }
}


