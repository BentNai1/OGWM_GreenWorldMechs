using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MechPuppetController), true)]
[CanEditMultipleObjects] // Allows multi-object selection
public class MechPuppetEditorButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draws default fields

        // Get reference to the target script (could be any of the 3 types)
        Object targetScript = target;

        if (GUILayout.Button("Update Visuals"))
        {
            // Call the UpdateVisuals() method on whichever script this is
            var method = targetScript.GetType().GetMethod("UpdateVisuals");
            if (method != null)
            {
                method.Invoke(targetScript, null);
                EditorUtility.SetDirty(targetScript); // Marks object as modified
            }
        }
    }
}
