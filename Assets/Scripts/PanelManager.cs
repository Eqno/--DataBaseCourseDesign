using System.Collections.ObjectModel;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    private void Start()
    {
        Dialog = GameObject.Find("弹窗");
        Dialog.SetActive(false);
    }
    public void StorePanelToWelcomePanel()
    {
        
    }
    public void StorePanelToMenuPanel()
    {
        MenuSlide.slideDirection = false;
        GameObject.Find("菜单界面").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width - 1, 0);
    }
    public void MenuPanelToStorePanel()
    {
        MenuSlide.returnButton = true;
    }
    public void MenuPanelToOrderPanel()
    {
        OrderSlide.slideDirection = false;
        GameObject.Find("订单界面").GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width - 1, 0);
    }
    public void OrderPanelToMenuPanel()
    {
        OrderSlide.returnButton = true;
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
