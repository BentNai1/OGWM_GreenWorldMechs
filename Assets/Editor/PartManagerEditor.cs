using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MechPuppetPartController))]
public class PartManagerEditor : Editor
{
    private void OnSceneGUI()
    {
        MechPuppetPartController mechPuppetPartController = (MechPuppetPartController)target;

        if (mechPuppetPartController.hardPoints == null || mechPuppetPartController.hardPointPositions == null)
            return;

        Transform transform = mechPuppetPartController.transform;

        // --- Draw Hardpoints ---
        for (int i = 0; i < mechPuppetPartController.hardPoints.Length; i++)
        {
            MechPuppetPartController.PartType hardPoint = mechPuppetPartController.hardPoints[i];
            Vector2 localPosition = mechPuppetPartController.hardPointPositions[i];
            Vector3 worldPosition = transform.position + (Vector3)localPosition;

            // Get color from dictionary
            Color pointColor = mechPuppetPartController.partColors.ContainsKey(hardPoint) ? mechPuppetPartController.partColors[hardPoint] : Color.white;
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
                Undo.RecordObject(mechPuppetPartController, "Move Hardpoint Position");
                mechPuppetPartController.hardPointPositions[i] = newWorldPos - transform.position;
            }
        }

        // --- Draw Self Marker ---
        MechPuppetPartController.PartType selfType = mechPuppetPartController.partType;  // Get self part type
        if (mechPuppetPartController.partColors.TryGetValue(selfType, out Color selfColor))
        {
            Handles.color = selfColor;

            // Draw marker at object's position
            Handles.DrawWireDisc(transform.position, Vector3.forward, 0.15f);
            Handles.Label(transform.position + Vector3.up * 0.2f, selfType.ToString(),
                new GUIStyle() { normal = new GUIStyleState() { textColor = selfColor }, fontStyle = FontStyle.Bold });
        }
    }
}
