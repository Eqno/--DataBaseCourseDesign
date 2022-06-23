using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public static string store;
    public GameObject MenuItem;
    private GameObject Content;
    private RectTransform itemTransform;
    public void ShowMenu()
    {
        Content = GameObject.Find("MenuContent");
        itemTransform = MenuItem.GetComponent<RectTransform>();
        ClearContent();
        
        store = EventSystem.current.currentSelectedGameObject.name;
        List<List<string>> foods = SqlCache.ListFoods(store);
        for (int i=0; i<foods.Count; i++)
        {
            GameObject item = Instantiate(MenuItem);
            item.transform.SetParent(Content.transform);
            string food = foods[i][0];
            item.name = food;

            RectTransform recTran = item.GetComponent<RectTransform>();
            recTran.sizeDelta = itemTransform.sizeDelta;
            recTran.anchoredPosition = new Vector2(0, itemTransform.anchoredPosition.y-i*itemTransform.sizeDelta.y);

            Text[] texts = item.GetComponentsInChildren<Text>();
            texts[0].text = food;
            texts[1].text = foods[i][1]+"元/份";

            OrderManager.QuaryDictionary(store, food);
            texts[2].text = "共"+OrderManager.order[store][food].ToString()+"份";
        }
        Content.GetComponent<RectTransform>().sizeDelta
            = new Vector2(itemTransform.sizeDelta.x, foods.Count*itemTransform.sizeDelta.y);
        PanelManager.StorePanelToMenuPanel();
    }
    private void ClearContent()
    {
        Transform[] objs = Content.GetComponentsInChildren<Transform>();
        for (int i=1; i<objs.Length; i++) Destroy(objs[i].gameObject);
        Content.GetComponent<RectTransform>().sizeDelta
            = new Vector2(itemTransform.sizeDelta.x, 0);
    }
}
