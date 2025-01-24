using UnityEngine;
using System.Collections.Generic;

public class MechPuppetPartController : MonoBehaviour
{
    public enum PartType { Legs, Core, Head, RArm, LArm, LWeapon, RWeapon, RShoulder, LShoulder, Generator, Targetting, Booster, Extension}

    [SerializeField] private PartType _partType;
    [SerializeField] private PartType[] _hardPoints;

    //for setting defaults
    private Dictionary<PartType, PartType[]> defaultHardPoints = new Dictionary<PartType, PartType[]>()
    {
        { PartType.Legs, new PartType[] { PartType.Core, PartType.Booster } },
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
    };

    private void OnValidate()
    {
        if (defaultHardPoints.TryGetValue(_partType, out PartType[] defaults))
        {
            _hardPoints = defaults; // Assign default hardpoints when _partType changes
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
