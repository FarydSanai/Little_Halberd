using UnityEditor;

namespace LittleHalberd
{
    [CustomEditor(typeof(GraphSpriteEnabler))]
    public class GraphSpriteEnablerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (UnityEngine.GUILayout.Button("Enable sprites at Graph Nodes"))
            {
                GraphSpriteEnabler spriteEnabler = (GraphSpriteEnabler)target;
                spriteEnabler.EnableSpriteAtGraphNodes();
            }
        }
    }
}
