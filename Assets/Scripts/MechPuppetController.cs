using UnityEngine;
using System.Collections.Generic;

using static MechPuppetPartController;
public class MechPuppetController : MonoBehaviour
{
    [SerializeField] MechPuppetSlotController[] mechPuppetSlotControllers;
    List<MechPuppetSlotController> _slotsPendingUpdate = new List<MechPuppetSlotController>();
    

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //used by inspector button
    /// <summary>
    /// Updates sprite images inside this puppet, as well as their positions.
    /// </summary>
    public void UpdateVisuals()
    {
        foreach (var item in mechPuppetSlotControllers)
        {
            item.UpdateSprite(this);
        }
    }

    /// <summary>
    /// Iterate through slots, placing them from the ground up, and then updating positions of child pieces.
    /// 1 Find next piece
    /// 2 Update positions of dependent parts
    /// </summary>
    public void UpdateAllPartPositions()
    {
        PartType partQuery = PartType.Ground_NotAPart;
        PartType querryHasSlot = PartType.Legs;
        _slotsPendingUpdate.Clear();
        _slotsPendingUpdate.AddRange(mechPuppetSlotControllers);
        Vector2 pos = new Vector2(0, -5.4f); //TODO - update to change dynamically based on player screenspace, maybe?
        string logMessage = "UpdateAllPartPositions Complete. ";
        bool foundNoMatch = false; 

        while (_slotsPendingUpdate.Count > 0 && !foundNoMatch)
        {
            foundNoMatch = true; // a successful find sets this to false, allowing a new loop - prevents infinite loops
            foreach (var item in _slotsPendingUpdate)
            {
                if (item.GetPartController().IsNotAValidPart())
                    _slotsPendingUpdate.Remove(item);

                if (item.GetPartController().HasHardpointAsPartType(querryHasSlot, partQuery))
                {
                    foundNoMatch = false;

                    _slotsPendingUpdate.Remove(item);
                }
            }
        }

        Debug.Log(logMessage);
    }
}
