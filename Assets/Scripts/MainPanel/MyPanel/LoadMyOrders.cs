using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMyOrders : MonoBehaviour
{
    public GameObject _MyItem;
    private static GameObject MyItem, Content;
    private static RectTransform itemTransform;
    void Start()
    {
        MyItem = _MyItem;
        Content = GameObject.Find("MyContent");
        itemTransform = MyItem.GetComponent<RectTransform>();
    }
    public static void LoadOrders(string username, string password)
    {
        ClearContent();
        List<List<string>> orders = SqlCache.ListOrders(username, password);
        for (int i=0; i<orders.Count; i++)
        {
            GameObject item = Instantiate(MyItem);
            item.transform.SetParent(Content.transform);
            item.name = orders[i][0];

            RectTransform recTran = item.GetComponent<RectTransform>();
            recTran.sizeDelta = itemTransform.sizeDelta;
            recTran.anchoredPosition = new Vector2(0, itemTransform.anchoredPosition.y-i*itemTransform.sizeDelta.y);

            Text[] texts = item.GetComponentsInChildren<Text>();
            texts[0].text = "订单"+orders[i][0];
            texts[1].text = "送至："+orders[i][1];
        }
        Content.GetComponent<RectTransform>().sizeDelta
            = new Vector2(itemTransform.sizeDelta.x, orders.Count*itemTransform.sizeDelta.y);
    }
    private static void ClearContent()
    {
        Transform[] objs = Content.GetComponentsInChildren<Transform>();
        for (int i=1; i<objs.Length; i++) Destroy(objs[i].gameObject);
        Content.GetComponent<RectTransform>().sizeDelta
            = new Vector2(itemTransform.sizeDelta.x, 0);
    }
}
