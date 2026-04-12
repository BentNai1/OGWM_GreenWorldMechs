using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GarageUI_Responsive_Layout))]
public class GarageUI_Button_RefreshLayout : Editor
{
    public override void OnInspectorGUI() 
    {
        // Draw the default inspector (all your normal fields)
        DrawDefaultInspector();

        // Get reference to the target script
        GarageUI_Responsive_Layout script = (GarageUI_Responsive_Layout)target;

        GUILayout.Space(10);

        // Create button
        if (GUILayout.Button("Update Layout"))
        {
            script.UpdateLayout();

            // Mark scene dirty so changes persist
            EditorUtility.SetDirty(script);
        }
    }
}
