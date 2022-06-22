using UnityEngine;

public class LoadStore : MonoBehaviour
{
    public GameObject StoreItem;

    private RectTransform itemTransform;
    void Start()
    {
        itemTransform = StoreItem.GetComponent<RectTransform>();

        for (int i=0; i<2; i++)
        {
            GameObject item = Instantiate(StoreItem);
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
