using UnityEngine;
using System.IO;

/// <summary>
/// Read and write part data with JSON file "UserParts".
/// </summary>
public static class PartDataIO
{
    private static readonly string userPartsFolder = Path.Combine(Application.persistentDataPath, "UserParts");

    /// <summary>
    /// Save data from a MechPuppetPartController to a JSON file in the UserParts folder. Filename is based on partName.
    /// </summary>
    /// <param name="controller"></param>
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

    /// <summary>
    /// Load data and return it as a MechPuppetPartData object. Returns null if no file found.
    /// </summary>
    /// <param name="partName"></param>
    /// <returns></returns>
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
