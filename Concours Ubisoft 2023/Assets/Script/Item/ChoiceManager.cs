using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    public bool isTopDown;
    // Start is called before the first frame update
    public GameObject frameGameObjectTopDown;
    public GameObject frameGameObjectNormal;

    public ItemManager itemManager;



    private void Start()
    {
        StartCoroutine(loadItems());
    }

    IEnumerator loadItems()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().UpdateItem();
    }

    public void selectItem(Item item, bool isTopDown)
    {
        this.isTopDown = isTopDown;
        if (!isTopDown)
        {
            frameGameObjectTopDown.GetComponent<Frame>().Restore();
        }
        else
        {
            frameGameObjectNormal.GetComponent<Frame>().Restore();
        }

        //call....
        GameManager.instance.setChooseItem(item);
    }

    public void setItem(Item itemRoom, Item item1,Item item2)
    {
        frameGameObjectTopDown.GetComponent<Frame>().item = item1;
        frameGameObjectNormal.GetComponent<Frame>().item = item2;
        frameGameObjectTopDown.GetComponent<Frame>().UpdateSprite();
        frameGameObjectNormal.GetComponent<Frame>().UpdateSprite();
        itemManager.item = itemRoom;
        itemManager.UpdateSprite();
    }


}
