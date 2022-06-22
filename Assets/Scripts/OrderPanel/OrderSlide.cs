using UnityEngine;

public class OrderSlide : MonoBehaviour
{
    public int SlideStep = 5000;

    public static bool slideDirection, returnButton;  // right->true, left->false

    private bool mouseButton0;
    private Vector3 mousePosition;
    private int rightPoint, criticalPoint;
    private RectTransform panelTransform;
    void Start()
    {
        slideDirection = true;
        rightPoint = Screen.width;
        criticalPoint = rightPoint / 2;
        mouseButton0 = returnButton = false;
        mousePosition = new Vector3(0, 0, 0);
        panelTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (panelTransform.anchoredPosition.x == rightPoint)
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
                    , 0, rightPoint
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
            if (slideDirection && panelTransform.anchoredPosition.x < rightPoint)
            {   
                panelTransform.anchoredPosition = new Vector2(
                    LimitValue(
                        panelTransform.anchoredPosition.x
                        + SlideStep * Time.deltaTime,
                        0, rightPoint
                    ), 0
                );
            }
            else if (! slideDirection && panelTransform.anchoredPosition.x > 0)
            {
                panelTransform.anchoredPosition = new Vector2(
                    LimitValue(
                        panelTransform.anchoredPosition.x
                        - SlideStep * Time.deltaTime,
                        0, rightPoint
                    ), 0
                );
            }
        }
    }
    private float LimitValue(float value, float minv, float maxv)
    { return value < minv ? minv : value > maxv ? maxv : value; }
}