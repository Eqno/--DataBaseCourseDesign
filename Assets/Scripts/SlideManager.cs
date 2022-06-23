using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideManager : MonoBehaviour
{
    private static int SlideStep = 5000;
    public static void SlideLeft(bool slideDirection, RectTransform panelTransform)
    {
        if (slideDirection && panelTransform.anchoredPosition.x > -Screen.width)
        {
            panelTransform.anchoredPosition = new Vector2(
                LimitValue(
                    panelTransform.anchoredPosition.x
                    - SlideStep * Time.deltaTime,
                    -Screen.width, 0
                ), 0
            );
        }
        else if (! slideDirection && panelTransform.anchoredPosition.x < 0)
        {
            panelTransform.anchoredPosition = new Vector2(
                LimitValue(
                    panelTransform.anchoredPosition.x
                    + SlideStep * Time.deltaTime,
                    -Screen.width, 0
                ), 0
            );
        }
    }
    public static void SlideRight(ref bool slideDirection, RectTransform panelTransform,
        ref bool mouseButton0, ref bool returnButton, ref Vector3 mousePosition,
        ref bool preSlideEnable, ref bool thisSlideEnable)
    {
        if (panelTransform.anchoredPosition.x == Screen.width)
            return;
        if (thisSlideEnable && Input.GetMouseButton(0))
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
                    , 0, Screen.width
                ), 0
            );
            mousePosition = Input.mousePosition;
            if (panelTransform.anchoredPosition.x == Screen.width)
                preSlideEnable = true;
        }
        else
        {
            if (mouseButton0 == true)
            {
                if (panelTransform.anchoredPosition.x > Screen.width/2) slideDirection = true;
                else slideDirection = false;
                mouseButton0 = false;
            }
            if (returnButton)
            {
                returnButton = false;
                slideDirection = true;
            }
            if (slideDirection && panelTransform.anchoredPosition.x < Screen.width)
            {   
                panelTransform.anchoredPosition = new Vector2(
                    LimitValue(
                        panelTransform.anchoredPosition.x
                        + SlideStep * Time.deltaTime,
                        0, Screen.width
                    ), 0
                );
                if (panelTransform.anchoredPosition.x == Screen.width)
                    preSlideEnable = true;
            }
            else if (! slideDirection && panelTransform.anchoredPosition.x > 0)
            {
                panelTransform.anchoredPosition = new Vector2(
                    LimitValue(
                        panelTransform.anchoredPosition.x
                        - SlideStep * Time.deltaTime,
                        0, Screen.width
                    ), 0
                );
            }
        }
    }
    private static float LimitValue(float value, float minv, float maxv)
    { return value < minv ? minv : value > maxv ? maxv : value; }
}
