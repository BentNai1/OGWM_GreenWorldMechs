using UnityEngine;
using System.Collections.Generic;

using static MechPuppetPartController;
public class MechPuppetController : MonoBehaviour
{
    [SerializeField] MechPuppetSlotController[] mechPuppetSlotControllers;
    List<MechPuppetSlotController> _slotsPendingPlacement = new List<MechPuppetSlotController>();
    List<MechPuppetSlotController> _slotsPlaced = new List<MechPuppetSlotController>();
    List<MechPuppetSlotController> _slotsToRemove = new List<MechPuppetSlotController>();
    List<MechPuppetSlotController> _slotsToAdd = new List<MechPuppetSlotController>();
    List<PartType> _partsToFind = new List<PartType>();



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

        UpdateAllPartPositions();
    }

    /// <summary>
    /// Iterate through slots, placing them from the ground up, and then updating positions of child pieces.
    /// 1 Find next piece
    /// 2 Update positions of dependent parts
    /// </summary>
    public void UpdateAllPartPositions()
    {
        Vector2 pos = new Vector2(0, -5.4f); //TODO - update to change dynamically based on player screenspace, maybe?
        string logMessage = "UpdateAllPartPositions: ";
        bool foundMatch = false; //sentinal to prevent infinite loop in while

        //initialize with the ground, seeking legs
        PartType partQuery = PartType.Ground_NotAPart;
        PartType querryHasSlot = PartType.Legs;
        _slotsPendingPlacement.Clear();
        _slotsPlaced.Clear();
        _slotsToRemove.Clear();
        _slotsToAdd.Clear();
        _partsToFind.Clear();
        _slotsPendingPlacement.AddRange(mechPuppetSlotControllers);

        logMessage += _slotsPendingPlacement.Count + " slots to place. ";

        //find ground, clear unsusable parts
        foreach (var item in _slotsPendingPlacement)
        {
            //mark from list all invalid parts
            if (item.GetPartController().IsNotAValidPart())
            {
                logMessage += item.gameObject.name + " invalid; ";
                _slotsToRemove.Add(item);
            }

            //if ground found...
            if (item.GetPartController().HasHardpointAsPartType(querryHasSlot, partQuery))
            {
                _slotsPlaced.Add(item);
                _slotsToRemove.Add(item);
                foundMatch = true;
            }
        }

        //removed marked slots and ground from pending list
        foreach (var item in _slotsToRemove)
        {
            _slotsPendingPlacement.Remove(item);
        }
        _slotsToRemove.Clear();

        if (!foundMatch) 
        {
            logMessage += "- ERROR! No ground slot found. Can't place puppet!";
        }
        
        //Main placement loop
        while (_slotsPlaced.Count > 0 && foundMatch)
        {
            foundMatch = false; // a successful find sets this to true, allowing a new loop - otherwise prevents infinite while loops

            //loop through placed slots...
            foreach (var i_slot in _slotsPlaced)
            {
                _partsToFind.Clear();
                //loop through available hardpoints of current slot...
                _partsToFind.AddRange(i_slot.GetPartController().hardPoints);
                foreach (var hardpointToQuery in _partsToFind)
                {
                    //loop through unplaced slots...
                    foreach (var pendingSlot in _slotsPendingPlacement)
                    {
                        //... looking for an unplaced slot to match curent hardpoint of placed slot.
                        if (pendingSlot.GetPartController().HasHardpointAsPartType(i_slot.GetPartController().partType, hardpointToQuery))
                        {
                            //offset position of pending from hardpoint
                            Vector3 pendingOffsetPos = pendingSlot.transform.position + pendingSlot.GetPartController().GetHardpointPositionV3(i_slot.GetPartController().partType);
                            //target to move to (i_slots position with hardpoint offset)
                            Vector3 targetOffsetPos = i_slot.transform.position + i_slot.GetPartController().GetHardpointPositionV3(pendingSlot.GetPartController().partType);
                            //move sprite location to match slot
                            Debug.Log(pendingSlot.gameObject.name+", pending pos/ target pos: " + pendingOffsetPos + ", " + targetOffsetPos + ", vector: " + (targetOffsetPos - pendingOffsetPos));
                            pendingSlot.transform.position += targetOffsetPos - pendingOffsetPos;

                            //move placed slot to other list
                            _slotsToAdd.Add(pendingSlot);
                            logMessage += pendingSlot.gameObject.name + " moved; ";
                            foundMatch = true;
                        }
                    }
                }
                //all hardpoints checked, mark for removal
                _slotsToRemove.Add(i_slot);
            }

            //remove marked slots from placed list
            foreach (var item in _slotsToRemove)
            {
                _slotsPlaced.Remove(item);
            }
            _slotsToRemove.Clear();

            //add placed slots to list for next search iteration and remove from pending list
            foreach (var item in _slotsToAdd)
            {
                _slotsPlaced.Add(item);
                _slotsPendingPlacement.Remove(item);
            }
            _slotsToAdd.Clear();
        }
        logMessage += " Complete.";
        Debug.Log(logMessage);
    }
}
