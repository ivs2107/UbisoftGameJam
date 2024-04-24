using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Item> items;
    public Item itemRoom;
    public Item nextItem1;
    public Item  nextItem2;

    public bool hasSelected;

    public GameObject enemiesList;

    public TMP_Text text;

    

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesList != null)
        {
          
            if (enemiesList.transform.childCount == 0)
            {
                //GameObject.FindGameObjectWithTag("EntryBorder").GetComponent<Animator>().enabled = true;
                Animator animator = GameObject.FindGameObjectWithTag("EntryBorder").GetComponent<Animator>();
                //animator.speed = 0;
                animator.SetFloat("Speed", -1);
                //animator.updateMode = AnimatorUpdateMode.UnscaledTime;
                //animator.Play("BorderClose");
                enemiesList = null;
                text.text = "Reach the end of the level";
            }
            else
            {
                text.text = "Kill all enemies (" + enemiesList.transform.childCount + ")";
            }
        }
        else
        {
            text.text = "Reach the end of the level";
        }
    }

    public void setEnemiesList()
    {
        enemiesList = GameObject.FindGameObjectWithTag("EnemiesList");
    }


    public void setChooseItem(Item item)
    {
        itemRoom = item;
        items.Remove(item);
        hasSelected = true;
    }


    public Item getItem(List<Item> itemsCopy)
    {
       
        int randomNumber = Random.Range(0, itemsCopy.Count);
        Item item = itemsCopy[randomNumber];
        itemsCopy.RemoveAt(randomNumber);
        return item;
    }

    public void UpdateItem()
    {
        hasSelected = false;
        if (items.Count <= 1)
        {
            nextItem1 = null;
            nextItem2 = null;
        }
        else
        {
            List<Item> itemsCopy = new List<Item>(items);
            nextItem1 = getItem(itemsCopy);
            nextItem2 = getItem(itemsCopy);
        }
        GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ChoiceManager>().setItem(itemRoom, nextItem1, nextItem2);
    }

}
