using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOrder : MonoBehaviour
{
    public GameObject OrderItem;

    private RectTransform itemTransform;
    void Start()
    {
        itemTransform = OrderItem.GetComponent<RectTransform>();

        for (int i=0; i<10; i++)
        {
            GameObject item = Instantiate(OrderItem);
            item.transform.SetParent(this.transform);
            RectTransform recTran = item.GetComponent<RectTransform>();
            recTran.sizeDelta = itemTransform.sizeDelta;
            recTran.anchoredPosition = new Vector2(0, itemTransform.anchoredPosition.y-i*itemTransform.sizeDelta.y);
        }
    }
    void Update()
    {
        
    }
}
