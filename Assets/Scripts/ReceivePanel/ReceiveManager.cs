using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReceiveManager : MonoBehaviour
{
    public GameObject ReceiveItem;
    private static int orderNum;
    private GameObject Content;
    private RectTransform itemTransform;
    public void ShowReceive()
    {
        Content = GameObject.Find("ReceiveContent");
        itemTransform = ReceiveItem.GetComponent<RectTransform>();
        ClearContent();
        
        orderNum = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        List<List<string>> foods = SqlCache.ListOrderItems(LogIn.usr, LogIn.pwd, orderNum);
        for (int i=0; i<foods.Count; i++)
        {
            GameObject item = Instantiate(ReceiveItem);
            item.transform.SetParent(Content.transform);

            RectTransform recTran = item.GetComponent<RectTransform>();
            recTran.sizeDelta = itemTransform.sizeDelta;
            recTran.anchoredPosition = new Vector2(0, itemTransform.anchoredPosition.y-i*itemTransform.sizeDelta.y);

            Text[] texts = item.GetComponentsInChildren<Text>();
            texts[0].text = foods[i][0];
            texts[1].text = foods[i][1];
            texts[2].text = "共"+foods[i][2]+"份";
        }
        Content.GetComponent<RectTransform>().sizeDelta
            = new Vector2(itemTransform.sizeDelta.x, foods.Count*itemTransform.sizeDelta.y);
        PanelManager.MyPanelToReceivePanel();
    }
    private void ClearContent()
    {
        Transform[] objs = Content.GetComponentsInChildren<Transform>();
        for (int i=1; i<objs.Length; i++) Destroy(objs[i].gameObject);
        Content.GetComponent<RectTransform>().sizeDelta
            = new Vector2(itemTransform.sizeDelta.x, 0);
    }
    public void ConfirmReceive()
    {
        SqlCache.RemoveOrder(LogIn.usr, LogIn.pwd, orderNum);
        LoadMyOrders.LoadOrders(LogIn.usr, LogIn.pwd);
        ReceiveSlide.returnButton = InnerSlide.slideEnable = true;
        StartCoroutine(PanelManager.MakeDialog("确认收货成功！"));
    }
}
