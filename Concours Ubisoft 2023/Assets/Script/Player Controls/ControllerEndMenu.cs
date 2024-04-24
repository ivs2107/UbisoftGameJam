using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllerEndMenu : MonoBehaviour
{

    public List<Button> listButtons;
    private int index = 0;
    public TMP_Text selectedText;

    private float startTime;
    private bool canCall = false;
    void Start()
    {
        startTime = 0;
    }

    public void SoundSelect()
    {
        AudioManager.instance.Play("Select");
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
                listButtons[index].GetComponentInChildren<TMP_Text>().color = Color.white;
                listButtons[index].GetComponent<Image>().color = Color.white;

                index++;
                if (index == listButtons.Count)
                {
                    index = 0;
                }
                listButtons[index].GetComponentInChildren<TMP_Text>().color = Color.yellow;
                listButtons[index].GetComponent<Image>().color = Color.yellow;
                AudioManager.instance.Play("Select");
                canCall = false;
            }
            else if (vector.x < -0.9f)
            {
                listButtons[index].GetComponentInChildren<TMP_Text>().color = Color.white;
                listButtons[index].GetComponent<Image>().color = Color.white;
                if (index == 0)
                {
                    index = listButtons.Count;
                }
                index--;

                listButtons[index].GetComponentInChildren<TMP_Text>().color = Color.yellow;
                listButtons[index].GetComponent<Image>().color = Color.yellow;
                AudioManager.instance.Play("Select");
                canCall = false;
            }
        }

    }

    public void ActivateButton()
    {
        if (this.gameObject.activeSelf&& canCall)
        {
            AudioManager.instance.Play("SelectPlay");
            listButtons[index].onClick.Invoke();
        }
    }
}
