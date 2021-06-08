using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class GraphSpriteEnabler : MonoBehaviour
    {
        public bool NodeSpritesEnabled;
        public void EnableSpriteAtGraphNodes()
        {
            SpriteRenderer[] spritesRend = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
            
            if (NodeSpritesEnabled)
            {
                foreach (SpriteRenderer sr in spritesRend)
                {
                    sr.enabled = false;
                    NodeSpritesEnabled = false;
                }
            }
            else
            {
                foreach (SpriteRenderer sr in spritesRend)
                {
                    sr.enabled = true;
                    NodeSpritesEnabled = true;
                }
            }

        }
    }
}