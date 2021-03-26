using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class SpriteChanger : MonoBehaviour
    {
        public Sprite sprite;
        public void ChangeSpritesInChilren()
        {
            SpriteRenderer[] spritesRend = this.gameObject.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer sr in spritesRend)
            {
                sr.sprite = sprite;
            }
        }
    }
}
