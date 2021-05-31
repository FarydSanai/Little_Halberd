using UnityEditor;
using UnityEngine;

namespace LittleHalberd.InspectorUI
{
    //public class ColorSpacer : PropertyAttribute
    //{
    //    public float spaceHeight;
    //    public float lineHeight;
    //    public float lineWidth;
    //    public Color lineColor = Color.red;

    //    public ColorSpacer(float spaceHeight, float lineHeight, float lineWidth, float r, float g, float b)
    //    {
    //        this.spaceHeight = spaceHeight;
    //        this.lineHeight = lineHeight;
    //        this.lineWidth = lineWidth;

    //        this.lineColor = new Color(r, g, b);
    //    }
    //}

    //[CustomPropertyDrawer(typeof(ColorSpacer))]
    //public class ColorSpacerDrawer : DecoratorDrawer
    //{
    //    ColorSpacer colorSpacer
    //    {
    //        get { return ((ColorSpacer)attribute); }
    //    }

    //    public override float GetHeight()
    //    {
    //        return base.GetHeight() + colorSpacer.spaceHeight;
    //    }

    //    public override void OnGUI(Rect position)
    //    {
    //        // calculate the rect values for where to draw the line in the inspector
    //        float lineX = (position.x + (position.width / 2)) - colorSpacer.lineWidth / 2;
    //        float lineY = position.y + (colorSpacer.spaceHeight / 2);
    //        float lineWidth = colorSpacer.lineWidth;
    //        float lineHeight = colorSpacer.lineHeight;

    //        Color oldGuiColor = GUI.color;
    //        GUI.color = colorSpacer.lineColor;
    //        //EditorGUI.DrawPreviewTexture(new Rect(lineX, lineY, lineWidth, lineHeight), Texture2D.redTexture);
    //        EditorGUI.DrawRect(new Rect(lineX, lineY, lineWidth, lineHeight), colorSpacer.lineColor);
    //        GUI.color = oldGuiColor;
    //    }
    //}
}
