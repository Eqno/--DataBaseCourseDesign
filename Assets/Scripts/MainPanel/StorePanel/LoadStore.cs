using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadStore : MonoBehaviour
{
    public GameObject StoreItem;

    private RectTransform itemTransform;
    void Start()
    {
        itemTransform = StoreItem.GetComponent<RectTransform>();
        List<List<string>> stores = SqlCache.ListStores();
        for (int i=0; i<stores.Count; i++)
        {
            GameObject item = Instantiate(StoreItem);
            item.transform.SetParent(this.transform);
            item.name = stores[i][0];

            RectTransform recTran = item.GetComponent<RectTransform>();
            recTran.sizeDelta = itemTransform.sizeDelta;
            recTran.anchoredPosition = new Vector2(0, itemTransform.anchoredPosition.y-i*itemTransform.sizeDelta.y);

            Text[] texts = item.GetComponentsInChildren<Text>();
            texts[0].text = stores[i][0];
            texts[1].text = "营业时间：\n周一到周日"+stores[i][1]+"点至"+stores[i][2]+"点";
        }
        GetComponent<RectTransform>().sizeDelta
            = new Vector2(itemTransform.sizeDelta.x, stores.Count*itemTransform.sizeDelta.y);
    }
}
