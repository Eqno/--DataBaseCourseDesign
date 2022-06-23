using UnityEngine;

public class ReceiveSlide : MonoBehaviour
{
    public static bool slideDirection, returnButton, slideEnable;  // right->true, left->false

    private bool mouseButton0;
    private Vector3 mousePosition;
    private RectTransform panelTransform;
    void Start()
    {
        slideDirection = true;
        mousePosition = new Vector3(0, 0, 0);
        panelTransform = GetComponent<RectTransform>();
        mouseButton0 = returnButton = slideEnable = false;
    }
    void Update()
    {
        SlideManager.SlideRight(ref slideDirection, panelTransform,
            ref mouseButton0, ref returnButton, ref mousePosition,
            ref InnerSlide.slideEnable, ref slideEnable);
    }
    private float LimitValue(float value, float minv, float maxv)
    { return value < minv ? minv : value > maxv ? maxv : value; }
}