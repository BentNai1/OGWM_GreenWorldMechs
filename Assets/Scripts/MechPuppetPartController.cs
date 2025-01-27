using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MechPuppetPartController : MonoBehaviour
{
    public enum PartType { Legs, Core, Head, RArm, LArm, LWeapon, RWeapon, RShoulder, LShoulder, Generator, Targetting, Booster, Extension, Ground_NotAPart, Empty_NotAPart}

    [SerializeField] private PartType _partType;
    public PartType partType
    {
        get { return _partType; }
    }
    [SerializeField] private PartType[] _hardPoints;
    public PartType[] hardPoints
    {
        get { return _hardPoints; }
        set { _hardPoints = value; }
    }
    [SerializeField] private Vector2[] _hardPointPositions; // Positions relative to the object
    public Vector2[] hardPointPositions
    {
        get { return _hardPointPositions; }
        set { _hardPointPositions = value; }
    }
    [SerializeField] private Sprite _sprite;
    public Sprite sprite
    {
        get { return _sprite; }
    }

    #region Inspector Autofill Dictionaries
    //for setting defaults
    public Dictionary<PartType, PartType[]> defaultHardPoints = new Dictionary<PartType, PartType[]>()
    {
        { PartType.Legs, new PartType[] { PartType.Core, PartType.Booster, PartType.Ground_NotAPart } },
        { PartType.Core, new PartType[] { PartType.Legs, PartType.Head, PartType.RArm, PartType.LArm, PartType.RShoulder,PartType.LShoulder,PartType.Generator,PartType.Targetting,PartType.Extension } },
        { PartType.Head, new PartType[] { PartType.Core} },
        { PartType.LArm, new PartType[] { PartType.Core, PartType.LWeapon } },
        { PartType.RArm, new PartType[] { PartType.Core, PartType.RWeapon } },
        { PartType.LWeapon, new PartType[] { PartType.Core, PartType.LArm } },
        { PartType.RWeapon, new PartType[] { PartType.Core, PartType.RArm } },
        { PartType.RShoulder, new PartType[] { PartType.Core} },
        { PartType.LShoulder, new PartType[] { PartType.Core} },
        { PartType.Generator, new PartType[] { PartType.Core} },
        { PartType.Targetting, new PartType[] { PartType.Core} },
        { PartType.Booster, new PartType[] { PartType.Legs} },
        { PartType.Extension, new PartType[] { PartType.Core} },
        { PartType.Ground_NotAPart, new PartType[] { PartType.Legs} },
        { PartType.Empty_NotAPart, new PartType[] {} },
    };

    // Define colors for each PartType
    public Dictionary<PartType, Color> partColors = new Dictionary<PartType, Color>
    {
        { PartType.Legs, Color.green },
        { PartType.Core, Color.blue },
        { PartType.Head, Color.red },
        { PartType.RArm, Color.yellow },
        { PartType.LArm, Color.magenta },
        { PartType.LWeapon, Color.cyan },
        { PartType.RWeapon, Color.cyan },
        { PartType.RShoulder, Color.white },
        { PartType.LShoulder, Color.gray },
        { PartType.Generator, new Color(1f, 0.5f, 0f) }, // Orange
        { PartType.Targetting, new Color(0.5f, 0.2f, 0.8f) }, // Purple
        { PartType.Booster, new Color(0.8f, 0.3f, 0.3f) }, // Dark Red
        { PartType.Extension, new Color(0.3f, 0.7f, 0.3f) } // Dark Green
    };
    #endregion

    private void OnValidate()
    {
        if (defaultHardPoints.TryGetValue(_partType, out PartType[] defaults))
        {
            _hardPoints = defaults; // Assign default hardpoints when _partType changes
        }

        // Ensure _hardPointPositions matches _hardPoints length
        if (_hardPoints != null)
        {
            if (_hardPointPositions == null || _hardPointPositions.Length != _hardPoints.Length)
            {
                _hardPointPositions = new Vector2[_hardPoints.Length];
            }
        }
    }
}
