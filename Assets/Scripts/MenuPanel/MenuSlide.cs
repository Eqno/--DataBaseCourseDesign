using UnityEngine;

public class MenuSlide : MonoBehaviour
{
    public int SlideStep = 5000;

    public static bool slideDirection, returnButton;  // right->true, left->false

    private bool mouseButton0;
    private Vector3 mousePosition;
    private int RightPoint, criticalPoint;
    private RectTransform panelTransform;
    void Start()
    {
        slideDirection = true;
        RightPoint = Screen.width;
        criticalPoint = RightPoint / 2;
        mouseButton0 = returnButton = false;
        mousePosition = new Vector3(0, 0, 0);
        panelTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (panelTransform.anchoredPosition.x == RightPoint)
            return;
        if (Input.GetMouseButton(0))
        {
            if (mouseButton0 == false)
            {
                mousePosition = Input.mousePosition;
                mouseButton0 = true;
            }
            panelTransform.anchoredPosition = new Vector2(
                LimitValue(
                    panelTransform.anchoredPosition.x
                    + Input.mousePosition.x
                    - mousePosition.x
                    , 0, RightPoint
                ), 0
            );
            mousePosition = Input.mousePosition;
        }
        else
        {
            if (mouseButton0 == true)
            {
                if (panelTransform.anchoredPosition.x > criticalPoint) slideDirection = true;
                else slideDirection = false;
                mouseButton0 = false;
            }
            if (returnButton)
            {
                returnButton = false;
                slideDirection = true;
            }
            if (slideDirection && panelTransform.anchoredPosition.x < RightPoint)
            {   
                panelTransform.anchoredPosition = new Vector2(
                    LimitValue(
                        panelTransform.anchoredPosition.x
                        + SlideStep * Time.deltaTime,
                        0, RightPoint
                    ), 0
                );
            }
            else if (! slideDirection && panelTransform.anchoredPosition.x > 0)
            {
                panelTransform.anchoredPosition = new Vector2(
                    LimitValue(
                        panelTransform.anchoredPosition.x
                        - SlideStep * Time.deltaTime,
                        0, RightPoint
                    ), 0
                );
            }
        }
    }
    private float LimitValue(float value, float minv, float maxv)
    { return value < minv ? minv : value > maxv ? maxv : value; }
}