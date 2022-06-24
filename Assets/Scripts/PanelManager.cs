using System.Collections.ObjectModel;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PanelManager : MonoBehaviour
{
    void Start()
    {
        Dialog = GameObject.Find("弹窗");
        Dialog.SetActive(false);
    }
    public static void StorePanelToMenuPanel()
    {
        MenuSlide.slideDirection = InnerSlide.slideEnable = false;
        GameObject.Find("菜单界面").GetComponent<RectTransform>()
            .anchoredPosition = new Vector2(Screen.width - 1, 0);
    }
    public void MenuPanelToStorePanel()
    { MenuSlide.returnButton = InnerSlide.slideEnable = true; }
    public static void MenuPanelToOrderPanel()
    {
        OrderSlide.slideDirection = MenuSlide.slideEnable = false;
        GameObject.Find("订单界面").GetComponent<RectTransform>()
            .anchoredPosition = new Vector2(Screen.width - 1, 0);
    }
    public void OrderPanelToMenuPanel()
    { OrderSlide.returnButton = MenuSlide.slideEnable = true; }
    public static void OrderPanelToGitftPanel()
    {
        GiftSlide.slideDirection = OrderSlide.slideEnable = false;
        GameObject.Find("赠品界面").GetComponent<RectTransform>()
            .anchoredPosition = new Vector2(Screen.width - 1, 0);
    }
    public static void GiftPanelToMyPanel()
    {
        GiftSlide.returnButton = OrderSlide.slideEnable = true;
        OrderSlide.returnButton = MenuSlide.slideEnable = true;
        MenuSlide.returnButton = InnerSlide.slideEnable = true;
    }
    public static void MyPanelToReceivePanel()
    {
        ReceiveSlide.slideEnable = true;
        ReceiveSlide.slideDirection = InnerSlide.slideEnable = false;
        GameObject.Find("收货界面").GetComponent<RectTransform>()
            .anchoredPosition = new Vector2(Screen.width - 1, 0);
    }
    public void ReceivePanelToMyPanel()
    { ReceiveSlide.returnButton = InnerSlide.slideEnable = true; }
    public void SwitchToStorePanel()
    {
        InnerSlide.nowAtPanel = 1;
        InnerSlide.switchButton = true;
    }
    public void SwitchToMyPanel()
    {
        InnerSlide.nowAtPanel = 2;
        InnerSlide.switchButton = true;
    }
    public void SwitchToBalancePanel()
    {
        InnerSlide.nowAtPanel = 3;
        InnerSlide.switchButton = true;
    }
    IEnumerator QuitApp()
    {
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
    public void CloseApp()
    {
        WelcomeSlide.slideEnable = WelcomeSlide.slideDirection = false;
        GameObject.Find("欢迎界面").GetComponent<RectTransform>()
            .anchoredPosition = new Vector2(0, Screen.height - 1);
        StartCoroutine(QuitApp());
    }
    
    public static int DialogTime = 2;
    public static GameObject Dialog;
    public static IEnumerator MakeDialog(string text)
    {
        Dialog.SetActive(true);
        Dialog.GetComponentInChildren<Text>().text = text;
        yield return new WaitForSeconds(DialogTime);
        Dialog.SetActive(false);
    }
}
