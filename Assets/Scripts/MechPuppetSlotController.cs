using UnityEngine;

public class MechPuppetSlotController : MonoBehaviour
{
    [Header("Slot References")]
    [Tooltip("The part controller associated with this slot.")]
    [SerializeField] private MechPuppetPartController _mechPuppetPart;

    [Tooltip("The sprite renderer used to display the part sprite.")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    // When loading, if missing references, find em.
    //private void OnValidate()
    //{
    //    if (_mechPuppetPart == null || _spriteRenderer == null)
    //    {
    //        FindMechPuppetController();
    //    }
    //}


    public MechPuppetPartController GetPartController()
    {
        return _mechPuppetPart;
    }

    public void UpdateSprite(MechPuppetController callingController)
    {
        // If either required reference is missing, attempt to find them automatically.
        if (_mechPuppetPart == null || _spriteRenderer == null)
        {
            FindMechPuppetController();
        }

        // If references are still missing after searching, stop and log a warning.
        if (_mechPuppetPart == null || _spriteRenderer == null)
        {
            Debug.LogWarning($"[{nameof(MechPuppetSlotController)}] Could not update sprite on '{gameObject.name}' because required references are missing.");
            return;
        }

        _spriteRenderer.sprite = _mechPuppetPart.sprite;
        _spriteRenderer.flipX = _mechPuppetPart.flipXSprite;
        _spriteRenderer.flipY = _mechPuppetPart.flipYSprite;
    }

    // Attempts to automatically find the part controller in this slot's children,
    // then finds the sprite renderer in that part controller's children.
    private void FindMechPuppetController()
    {
        // Search for the part controller.
        
        _mechPuppetPart = GetComponentInChildren<MechPuppetPartController>(true);

        if (_mechPuppetPart == null)
        {
            Debug.LogWarning($"[{nameof(MechPuppetSlotController)}] No {nameof(MechPuppetPartController)} found in children of '{gameObject.name}'.");
            return;
        }

        // Search for the sprite renderer.
        
        _spriteRenderer = _mechPuppetPart.GetComponentInChildren<SpriteRenderer>(true);

        if (_spriteRenderer == null)
        {
            Debug.LogWarning($"[{nameof(MechPuppetSlotController)}] No {nameof(SpriteRenderer)} found in children of '{_mechPuppetPart.gameObject.name}'.");
        }
        
    }
}
