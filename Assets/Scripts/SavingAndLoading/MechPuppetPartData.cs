using System;
using UnityEngine;

[Serializable]
public class MechPuppetPartData
{
    public string partName;
    public MechPuppetPartController.PartType partType;
    public MechPuppetPartController.PartType[] hardPoints;
    public Vector2[] hardPointPositions;
    public string spriteName; // We'll serialize the name of the sprite
    public bool flipXSprite;
    public bool flipYSprite;
}
