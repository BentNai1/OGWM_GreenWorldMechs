using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles conversion between MechPuppetPartController and MechPuppetPartData for saving and loading.
/// </summary>
public static class MechPuppetPartSerializer
{
    /// <summary>
    /// Save data and translate from controller to data
    /// </summary>
    /// <param name="controller"></param>
    /// <returns></returns>
    public static MechPuppetPartData ToData(MechPuppetPartController controller)
    {
        return new MechPuppetPartData
        {
            partName = controller.partName,
            partType = controller.partType,
            hardPoints = controller.hardPoints,
            hardPointPositions = controller.hardPointPositions,
            spriteName = controller.sprite != null ? controller.sprite.name : "",
            flipXSprite = controller.flipXSprite,
            flipYSprite = controller.flipYSprite
        };
    }

    /// <summary>
    /// Load data and translate from data to controller. Also apply the sprite based on the sprite name in the data.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="controller"></param>
    /// <param name="spriteLibrary"></param>
    public static void ApplyData(MechPuppetPartData data, MechPuppetPartController controller, Dictionary<string, Sprite> spriteLibrary)
    {
        controller.partName = data.partName;
        controller.hardPoints = data.hardPoints;
        controller.hardPointPositions = data.hardPointPositions;

        controller.GetType().GetField("_partType", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.SetValue(controller, data.partType);

        if (spriteLibrary.TryGetValue(data.spriteName, out var sprite))
        {
            controller.GetType().GetField("_sprite", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(controller, sprite);
        }

        controller.flipXSprite = data.flipXSprite;
        controller.flipYSprite = data.flipYSprite;
    }
}
