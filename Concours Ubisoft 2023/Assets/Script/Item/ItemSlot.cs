using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gridLayout;
    public void updateItemSlot(Sprite sprite)
    {
        GameObject go = new GameObject("Items");
        go.AddComponent<Image>();
        go.GetComponent<Image>().sprite = sprite;
        go.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        go.transform.parent = gridLayout.transform;
        //gridLayout.transform.SetSiblingIndex(go.transform.GetSiblingIndex());
    }
}
