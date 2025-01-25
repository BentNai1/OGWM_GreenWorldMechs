using UnityEngine;

public class MechPuppetSlotController : MonoBehaviour
{
    [SerializeField] private MechPuppetPartController _mechPuppetPart;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnValidate()
    {
        //if (_mechPuppetPart & _spriteRenderer)
        //    UpdateSprite();
        //else
        //    Debug.Log("Missing component(s) on MechPuppetSlotController");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSprite()
    {
        _spriteRenderer.sprite = _mechPuppetPart.sprite;
    }
}
