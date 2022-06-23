using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OrderManager : MonoBehaviour
{
    public GameObject OrderItem, Price, GiftItem;
    private int totPrice;
    private GameObject Content;
    private RectTransform itemTransform;
    public static Dictionary<string, Dictionary<string, int>> order
        = new Dictionary<string, Dictionary<string, int>>();
    public void Plus()
    {
        string store = MenuManager.store, food = gameObject.name;
        QuaryDictionary(store, food);
        order[store][food] ++;
        GetComponentsInChildren<Text>()[2].text = "共"+order[store][food].ToString()+"份";
    }
    public void Minus()
    {
        string store = MenuManager.store, food = gameObject.name;
        QuaryDictionary(store, food);
        if (order[store][food] > 0) order[store][food] --;
        else StartCoroutine(PanelManager.MakeDialog("最少可买0份！"));
        GetComponentsInChildren<Text>()[2].text = "共"+order[store][food].ToString()+"份";
    }
    public void CheckOut()
    {
        Content = GameObject.Find("OrderContent");
        itemTransform = OrderItem.GetComponent<RectTransform>();
        ClearContent();

        int cot = 0;
        totPrice = 0;
        foreach (var i in order)
        {
            foreach (var j in i.Value)
            {
                if (j.Value <= 0) continue;
                string store = i.Key, food = j.Key;
                string price = (j.Value*SqlCache.QuaryPrice(store, food)).ToString();
                totPrice += int.Parse(price);

                GameObject item = Instantiate(OrderItem);
                item.transform.SetParent(Content.transform);

                RectTransform recTran = item.GetComponent<RectTransform>();
                recTran.sizeDelta = itemTransform.sizeDelta;
                recTran.anchoredPosition = new Vector2(0, itemTransform.anchoredPosition.y-cot*itemTransform.sizeDelta.y);

                Text[] texts = item.GetComponentsInChildren<Text>();
                texts[0].text = store;
                texts[1].text = food;
                texts[2].text = "共"+price+"元";
                cot ++;
            }
        }
        Content.GetComponent<RectTransform>().sizeDelta
            = new Vector2(itemTransform.sizeDelta.x, cot*itemTransform.sizeDelta.y);

        Price.GetComponent<Text>().text = "总计："+totPrice.ToString()+"元";
        PanelManager.MenuPanelToOrderPanel();
    }
    public void ConfirmOrder()
    {
        if (LogIn.balance < totPrice)
            StartCoroutine(PanelManager.MakeDialog("余额不足！当前余额："+LogIn.balance.ToString()+"元"));
        else
        {
            LogIn.balance -= totPrice;
            SqlCache.UpdateBalance(LogIn.usr, LogIn.pwd, LogIn.balance);
            StartCoroutine(PanelManager.MakeDialog("支付成功！"));

        }
        GameObject.Find("金额").GetComponent<Text>().text = LogIn.balance.ToString()+"元";
        
        Content = GameObject.Find("GiftContent");
        itemTransform = GiftItem.GetComponent<RectTransform>();
        ClearContent();

        List<List<string>> gifts = SqlCache.ListGifts();
        for (int i=0; i<gifts.Count; i++)
        {
            GameObject item = Instantiate(GiftItem);
            item.transform.SetParent(Content.transform);

            RectTransform recTran = item.GetComponent<RectTransform>();
            recTran.sizeDelta = itemTransform.sizeDelta;
            recTran.anchoredPosition = new Vector2(0, itemTransform.anchoredPosition.y-i*itemTransform.sizeDelta.y);

            Text[] texts = item.GetComponentsInChildren<Text>();
            texts[0].text = gifts[i][0];
            texts[1].text = gifts[i][1];
        } 
        Content.GetComponent<RectTransform>().sizeDelta
            = new Vector2(itemTransform.sizeDelta.x, gifts.Count*itemTransform.sizeDelta.y);
        PanelManager.OrderPanelToGitftPanel();
    }
    public void Finish()
    {
        string addr = GameObject.Find("地址").GetComponentsInChildren<Text>()[1].text;
        if (addr.Length <= 0)
        {
            StartCoroutine(PanelManager.MakeDialog("地址不能为空！"));
            return;
        }
        List<string> tmp;
        List<List<string>> res = new List<List<string>>();
        foreach (var i in order)
        {
            foreach (var j in i.Value)
            {
                if (j.Value <= 0) continue;
                tmp = new List<string>();
                tmp.Add(i.Key);
                tmp.Add(j.Key);
                tmp.Add(j.Value.ToString());
                res.Add(tmp);
            }
        }
        order = new Dictionary<string, Dictionary<string, int>>();

        Text[] gift = EventSystem.current.currentSelectedGameObject.GetComponentsInChildren<Text>();
        tmp = new List<string>();
        tmp.Add(gift[0].text);
        tmp.Add(gift[1].text);
        tmp.Add("1");
        res.Add(tmp);

        SqlCache.AddOrder(LogIn.usr, LogIn.pwd, res, addr);
        LoadMyOrders.LoadOrders(LogIn.usr, LogIn.pwd);
        PanelManager.GiftPanelToMyPanel();
    }
    public static void QuaryDictionary(string store, string food)
    {
        if (! order.ContainsKey(store)) order.Add(store, new Dictionary<string, int>());
        if (! order[store].ContainsKey(food)) order[store].Add(food, 0);
    }
    private void ClearContent()
    {
        Transform[] objs = Content.GetComponentsInChildren<Transform>();
        for (int i=1; i<objs.Length; i++) Destroy(objs[i].gameObject);
        Content.GetComponent<RectTransform>().sizeDelta
            = new Vector2(itemTransform.sizeDelta.x, 0);
    }
}
