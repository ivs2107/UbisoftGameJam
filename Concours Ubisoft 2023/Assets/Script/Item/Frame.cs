using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour
{
    public ChoiceManager choiceManager;
    public GameObject squareWhite;
    public Item item;
    public SpriteRenderer spriteRenderer;
    public bool isTopDown;
    private bool isSelected = false;

    public void UpdateSprite()
    {
        spriteRenderer.sprite = null;
        if (item != null)
        {
            spriteRenderer.sprite = item.sprite;
        }



    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isSelected)
        {
            return;
        }
        isSelected = true;
        squareWhite.GetComponent<SpriteRenderer>().color = Color.yellow;
        //call..
        choiceManager.selectItem(item, isTopDown);
    }

    public void Restore()
    {
        isSelected = false;
        squareWhite.GetComponent<SpriteRenderer>().color = Color.white;
    }

}
