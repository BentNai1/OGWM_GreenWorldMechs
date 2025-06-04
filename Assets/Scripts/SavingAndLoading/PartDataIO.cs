using UnityEngine;
using System.IO;

public static class PartDataIO
{
    private static readonly string userPartsFolder = Path.Combine(Application.persistentDataPath, "UserParts");

    public static void SavePart(MechPuppetPartController controller)
    {
        if (!Directory.Exists(userPartsFolder))
            Directory.CreateDirectory(userPartsFolder);

        var data = MechPuppetPartSerializer.ToData(controller);
        string json = JsonUtility.ToJson(data, true);
        string filePath = Path.Combine(userPartsFolder, controller.partName + ".json");
        File.WriteAllText(filePath, json);

        Debug.Log($"Saved part to {filePath}");
    }

    public static MechPuppetPartData LoadPart(string partName)
    {
        string filePath = Path.Combine(userPartsFolder, partName + ".json");
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("No part found at: " + filePath);
            return null;
        }

        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<MechPuppetPartData>(json);
    }
}
