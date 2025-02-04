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
            if (!selectedType.ToString().Contains("NotAPart"))
            {
                EditorGUILayout.PropertyField(position, GUIContent.none);
            }
            else
            {
                EditorGUILayout.LabelField("(No Position)", GUILayout.Width(100));
            }

            EditorGUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
    }
}