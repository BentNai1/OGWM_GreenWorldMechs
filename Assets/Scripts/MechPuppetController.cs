using UnityEngine;

public class MechPuppetController : MonoBehaviour
{
    [SerializeField] MechPuppetSlotController[] mechPuppetSlotControllers;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //used by inspector button
    public void UpdateVisuals()
    {
        foreach (var item in mechPuppetSlotControllers)
        {
            item.UpdateSprite(this);
        }
    }
}
