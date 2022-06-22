using UnityEngine;

public class WelcomeSlide : MonoBehaviour
{
    public int SlideStep = 5000;

    public static bool slideDirection;  // up->true, down->false

    private bool mouseButton0;
    private Vector3 mousePosition;
    private int topPoint, criticalPoint;
    private RectTransform panelTransform;
    void Start()
    {
        topPoint = Screen.height;
        criticalPoint = topPoint / 2;
        mousePosition = new Vector3(0, 0, 0);
        mouseButton0 = slideDirection = false;
        panelTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (panelTransform.anchoredPosition.y == topPoint)
            return;
        if (Input.GetMouseButton(0))
        {
            if (mouseButton0 == false)
            {
                mousePosition = Input.mousePosition;
                mouseButton0 = true;
            }
            panelTransform.anchoredPosition = new Vector2(0,
                LimitValue(
                    panelTransform.anchoredPosition.y
                    + Input.mousePosition.y
                    - mousePosition.y
                    , 0, topPoint
                )
            );
            mousePosition = Input.mousePosition;
        }
        else
        {
            if (mouseButton0 == true)
            {
                if (panelTransform.anchoredPosition.y > criticalPoint) slideDirection = true;
                else slideDirection = false;
                mouseButton0 = false;
            }
            if (slideDirection && panelTransform.anchoredPosition.y < topPoint)
            {
                panelTransform.anchoredPosition = new Vector2(0,
                    LimitValue(
                        panelTransform.anchoredPosition.y
                        + SlideStep * Time.deltaTime,
                        0, topPoint
                    )
                );
            }
            else if (! slideDirection && panelTransform.anchoredPosition.y > 0)
            {
                panelTransform.anchoredPosition = new Vector2(0,
                    LimitValue(
                        panelTransform.anchoredPosition.y
                        - SlideStep * Time.deltaTime,
                        0, topPoint
                    )
                );
            }
        }
    }
    private float LimitValue(float value, float minv, float maxv)
    { return value < minv ? minv : value > maxv ? maxv : value; }
}