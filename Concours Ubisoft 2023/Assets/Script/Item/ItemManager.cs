using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item item;
    public TMP_Text textName;
    public TMP_Text description;
    public SpriteRenderer spriteRenderer;
    public GameObject itemText;
    // Start is called before the first frame update
    public void UpdateSprite()
    {
        itemText = GameObject.FindGameObjectWithTag("PanelItem");
        spriteRenderer.sprite = null;
        if (item != null)
        {
        spriteRenderer.sprite = item.sprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            item.itemAction.Invoke();
            itemText.transform.GetChild(0).gameObject.SetActive(true);
            itemText.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("itemPanel",-1,0f);
            itemText.GetComponent<ItemSlot>().updateItemSlot(item.sprite);
            textName = itemText.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>();
            description = itemText.transform.GetChild(0).GetChild(3).GetComponent<TMP_Text>();

            textName.text = item.itemName;
            description.text = item.description;


            AudioManager.instance.Play("PowerUp");
            Destroy(this.gameObject);

        }
    }
}
