using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelectWeapon : MonoBehaviour
{
    public List<Button> listButtons;
    public Button buttonBack;
    private int index = 0; 
    public void ActivateButton()
    {
        if (this.gameObject.activeSelf)
        {
            AudioManager.instance.Play("SelectPlay");
            listButtons[index].onClick.Invoke();
        }
    }
    public void ActivateBack()
    {
        if (this.gameObject.activeSelf)
        {
            AudioManager.instance.Play("SelectPlay");
            buttonBack.onClick.Invoke();
        }
    }

    private float startTime;
    private bool canCall = true;
    void Start()
    {
        startTime = 0;
    }
    private void Update()
    {

        if (startTime >= 0.2f)
        {
            startTime = 0;
            canCall = true;
        }
        if (!canCall)
        {
            startTime += Time.deltaTime;
        }

    }

    public void MoveCursor(Vector2 vector)
    {
        if (canCall && this.gameObject.activeSelf)
        {
            if (vector.x > 0.9f)
            {
                listButtons[index].GetComponent<Image>().color = Color.white;

                index++;
                if (index == listButtons.Count)
                {
                    index = 0;
                }
                listButtons[index].GetComponent<Image>().color = Color.yellow;
                AudioManager.instance.Play("Select");
                canCall = false;
            }
            else if (vector.x < -0.9f)
            {
                listButtons[index].GetComponent<Image>().color = Color.white;
                if (index == 0)
                {
                    index = listButtons.Count;
                }
                index--;

                listButtons[index].GetComponent<Image>().color = Color.yellow;
                AudioManager.instance.Play("Select");
                canCall = false;
            }
        }

    }
}
