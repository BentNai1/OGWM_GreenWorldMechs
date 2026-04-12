using UnityEngine;

/// <summary>
/// Checks for current screen aspect ratio, enables / disables objects accordingly.
/// </summary>
[ExecuteAlways]
public class GarageUI_Responsive_Layout : MonoBehaviour
{
    [SerializeField] GameObject layoutWide;
    [SerializeField] GameObject layoutTall;
    Vector2 lastScreenSize;
    float resolutionChangeTimer = 0f;
    float resolutionStabilityDelay = 0.5f;

    public enum LayoutMode
    {
        Wide,
        Tall
    }

    /// <summary>
    /// Get current layout mode
    /// </summary>
    /// <param name="CurrentLayout"></param>
    /// <returns></returns>
    public LayoutMode CurrentLayout { get; private set; }

    void Start()
    {
        UpdateLayout();
    }


    void Update()
    {
        Vector2 currentResolution = new Vector2(Screen.width, Screen.height);

        if (currentResolution != lastScreenSize)
        {
            lastScreenSize = currentResolution;
            resolutionChangeTimer = resolutionStabilityDelay;
        }

        if (resolutionChangeTimer > 0f)
        {
            resolutionChangeTimer -= Time.unscaledDeltaTime;

            if (resolutionChangeTimer <= 0f)
            {
                UpdateLayout();
            }
        }
    }

    public void UpdateLayout()
    {
        lastScreenSize = new Vector2(Screen.width, Screen.height);
        float aspect = (float)Screen.width / Screen.height;
        if( aspect < 1.3f)
            CurrentLayout = LayoutMode.Tall;
        else
            CurrentLayout = LayoutMode.Wide;

        SetOrientation();
    }

    void SetOrientation()
    {
        switch (CurrentLayout)
        {
            case LayoutMode.Tall:
                layoutTall.SetActive(true);
                layoutWide.SetActive(false);
                break;
            default: 
                layoutTall.SetActive(false);
                layoutWide.SetActive(true);
                break;
        }
    }
}