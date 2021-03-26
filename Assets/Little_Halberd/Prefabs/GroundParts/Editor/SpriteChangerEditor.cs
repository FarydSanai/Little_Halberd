using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LittleHalberd
{
    [CustomEditor(typeof(SpriteChanger))]
    public class SpriteChangerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Change sprites in children"))
            {
                SpriteChanger spriteChanger = (SpriteChanger)target;

                spriteChanger.ChangeSpritesInChilren();
            }
        }
    }
}
