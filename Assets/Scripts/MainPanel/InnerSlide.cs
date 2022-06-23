using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerSlide : MonoBehaviour
{
    public int SlideStep = 5000;
    public GameObject SlideBar, StorePanel, MyPanel, BalancePanel;
    
    public static int nowAtPanel;
    public static bool switchButton, slideEnable, logInEnable;

    private bool mouseButton0;
    private Vector3 mousePosition;
    private int borderPoint, criticalPoint;
    private RectTransform barRecTran, storeRecTran, myRecTran, balanceRecTran;
    void Start()
    {
        nowAtPanel = 2;  // 1->StorePanel, 2->MyPanel, 3->BalancePanel
        borderPoint = Screen.width;
        criticalPoint = borderPoint / 2;
        mousePosition = new Vector3(0, 0, 0);
        myRecTran = MyPanel.GetComponent<RectTransform>();
        barRecTran = SlideBar.GetComponent<RectTransform>();
        storeRecTran = StorePanel.GetComponent<RectTransform>();
        balanceRecTran = BalancePanel.GetComponent<RectTransform>();
        mouseButton0 = switchButton = slideEnable = logInEnable = false;

        UpdateOtherPanelPositon();
    }
    void Update()
    {
        if (logInEnable && slideEnable && Input.GetMouseButton(0))
        {
            if (mouseButton0 == false)
            {
                mousePosition = Input.mousePosition;
                mouseButton0 = true;
            }
            myRecTran.anchoredPosition = new Vector2(
                LimitValue(
                    myRecTran.anchoredPosition.x
                    + Input.mousePosition.x
                    - mousePosition.x
                    , -borderPoint, borderPoint
                ), 0
            );
            UpdateOtherPanelPositon();
            mousePosition = Input.mousePosition;
        }
        else
        {
            int temp = nowAtPanel;
            if (mouseButton0 == true)
            {
                if (myRecTran.anchoredPosition.x > criticalPoint) nowAtPanel = 1;
                else if (myRecTran.anchoredPosition.x < -criticalPoint) nowAtPanel = 3;
                else nowAtPanel = 2;
                mouseButton0 = false;
            }
            if (switchButton)
            {
                switchButton = false;
                nowAtPanel = temp;
            }
            if (nowAtPanel == 1 && myRecTran.anchoredPosition.x < borderPoint)
            {   
                myRecTran.anchoredPosition = new Vector2(
                    LimitValue(
                        myRecTran.anchoredPosition.x
                        + SlideStep * Time.deltaTime,
                        -borderPoint, borderPoint
                    ), 0
                );
                UpdateOtherPanelPositon();
            }
            else if (nowAtPanel == 2 && myRecTran.anchoredPosition.x > 0)
            {
                myRecTran.anchoredPosition = new Vector2(
                    LimitValue(
                        myRecTran.anchoredPosition.x
                        - SlideStep * Time.deltaTime,
                        0, borderPoint
                    ), 0
                );
                UpdateOtherPanelPositon();
            }
            else if (nowAtPanel == 2 && myRecTran.anchoredPosition.x < 0)
            {
                myRecTran.anchoredPosition = new Vector2(
                    LimitValue(
                        myRecTran.anchoredPosition.x
                        + SlideStep * Time.deltaTime,
                        -borderPoint, 0
                    ), 0
                );
                UpdateOtherPanelPositon();
            }
            else if (nowAtPanel == 3 && myRecTran.anchoredPosition.x > -borderPoint)
            {
                myRecTran.anchoredPosition = new Vector2(
                    LimitValue(
                        myRecTran.anchoredPosition.x
                        - SlideStep * Time.deltaTime,
                        -borderPoint, borderPoint
                    ), 0
                );
                UpdateOtherPanelPositon();
            }
        }
    }
    private float LimitValue(float value, float minv, float maxv)
    { return value < minv ? minv : value > maxv ? maxv : value; }
    private void UpdateOtherPanelPositon()
    {
        barRecTran.anchoredPosition = new Vector2(
            -myRecTran.anchoredPosition.x / borderPoint * SlideBar.GetComponent<RectTransform>().sizeDelta.x,
            barRecTran.anchoredPosition.y
        );
        storeRecTran.anchoredPosition = myRecTran.anchoredPosition - new Vector2(borderPoint, 0);
        balanceRecTran.anchoredPosition = myRecTran.anchoredPosition + new Vector2(borderPoint, 0);
    }
}
