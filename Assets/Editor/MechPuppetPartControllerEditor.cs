using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MechPuppetPartController))]
public class MechPuppetPartControllerEditor : Editor
{
    private void OnSceneGUI()
    {
        MechPuppetPartController controller = (MechPuppetPartController)target;

        if (controller.hardPoints == null || controller.hardPointPositions == null)
            return;

        Transform transform = controller.transform;

        for (int i = 0; i < controller.hardPoints.Length; i++)
        {
            MechPuppetPartController.PartType hardPoint = controller.hardPoints[i];
            Vector2 localPosition = controller.hardPointPositions[i];
            Vector3 worldPosition = transform.position + (Vector3)localPosition;

            // Get color from dictionary
            Color pointColor = controller.partColors.ContainsKey(hardPoint) ? controller.partColors[hardPoint] : Color.white;
            Handles.color = pointColor;

            // Draw sphere marker at world position
            Handles.DrawSolidDisc(worldPosition, Vector3.forward, 0.1f);

            // Label with part type
            Handles.Label(worldPosition + Vector3.up * 0.15f, hardPoint.ToString(), new GUIStyle() { normal = new GUIStyleState() { textColor = pointColor } });

            // Allow user to move marker in Scene View
            EditorGUI.BeginChangeCheck();
            Vector3 newWorldPos = Handles.PositionHandle(worldPosition, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(controller, "Move Hardpoint Position");
                controller.hardPointPositions[i] = newWorldPos - transform.position;
            }
        }
    }

    public override void OnInspectorGUI()
    {
        MechPuppetPartController controller = (MechPuppetPartController)target;

        serializedObject.Update();

        // Save and Load buttons
        
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Save"))
        {
            
        }

        if (GUILayout.Button("Load"))
        {
            
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("_partName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_partType"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_sprite"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("flipXSprite"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("flipYSprite"));

        SerializedProperty hardPointsProp = serializedObject.FindProperty("_hardPoints");
        SerializedProperty positionsProp = serializedObject.FindProperty("_hardPointPositions");

        EditorGUILayout.LabelField("Hardpoints", EditorStyles.boldLabel);

        if (hardPointsProp.arraySize != positionsProp.arraySize)
        {
            positionsProp.arraySize = hardPointsProp.arraySize;
        }

        for (int i = 0; i < hardPointsProp.arraySize; i++)
        {
            SerializedProperty hardPoint = hardPointsProp.GetArrayElementAtIndex(i);
            SerializedProperty position = positionsProp.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(hardPoint, GUIContent.none);

            MechPuppetPartController.PartType selectedType = (MechPuppetPartController.PartType)hardPoint.enumValueIndex;
            if (!selectedType.ToString().Contains("Empty"))
            {
                EditorGUILayout.PropertyField(position, GUIContent.none);
            }
            else
            {
                EditorGUILayout.LabelField("(No Position)", GUILayout.Width(100));
            }

            // Add a remove button for each hardpoint
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                hardPointsProp.DeleteArrayElementAtIndex(i);
                positionsProp.DeleteArrayElementAtIndex(i);
                serializedObject.ApplyModifiedProperties();
                return; // Prevents iterating over a modified list
            }

            EditorGUILayout.EndHorizontal();
        }

        // Add and Remove Buttons
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Hardpoint"))
        {
            hardPointsProp.arraySize++;
            positionsProp.arraySize++;

            // Initialize new hardpoint with default values
            SerializedProperty newHardPoint = hardPointsProp.GetArrayElementAtIndex(hardPointsProp.arraySize - 1);
            newHardPoint.enumValueIndex = 0; // Set default enum index

            SerializedProperty newPosition = positionsProp.GetArrayElementAtIndex(positionsProp.arraySize - 1);
            newPosition.vector2Value = Vector2.zero; // Default position

            serializedObject.ApplyModifiedProperties();
        }

        if (GUILayout.Button("Remove Last Hardpoint") && hardPointsProp.arraySize > 0)
        {
            hardPointsProp.arraySize--;
            positionsProp.arraySize--;
            serializedObject.ApplyModifiedProperties();
        }

        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }

}