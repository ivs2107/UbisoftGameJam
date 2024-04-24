using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public  List<Button> listButtons;
    private int index = 0;
    public TMP_Text selectedText;

    private float startTime;
    private bool canCall = true;
    void Start()
    {
        startTime = 0;
    }
    public void PlayGame ()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            AudioManager.instance.Play("Select");
        }

        public void QuitGame ()
        {
            Debug.Log("QUIT!");
            Application.Quit();
            AudioManager.instance.Play("Select");
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
            if (vector.y < -0.9f)
            {
                listButtons[index].GetComponentInChildren<TMP_Text>().color = Color.white;

                index++;
                if (index == listButtons.Count)
                {
                    index = 0;
                }
                listButtons[index].GetComponentInChildren<TMP_Text>().color = Color.yellow;
                AudioManager.instance.Play("Select");
                canCall = false;
            }
            else if(vector.y > 0.9f)
            {
                listButtons[index].GetComponentInChildren<TMP_Text>().color = Color.white;
                if(index == 0)
                {
                    index = listButtons.Count;
                }
                index--;

                listButtons[index].GetComponentInChildren<TMP_Text>().color = Color.yellow;
                AudioManager.instance.Play("Select");
                canCall = false;
            }
        }
    
    }

    public void ActivateButton()
    {
        if (this.gameObject.activeSelf)
        {
            AudioManager.instance.Play("SelectPlay");
            listButtons[index].onClick.Invoke();
        }
    }

    public void LoadScoresUnsaved()
    {
        string s = ReadString();
        if (s.Length == 0)
            return;
        var val = s.Split(" ");
        try
        {
            ServerManager.sendScore(val[0], val[1]);
            File.Delete("Assets/Resources/score.txt");
        }
        catch(WebException ex)
        {

        }
    }

    static string ReadString()

    {
        
        string path = "Assets/Resources/score.txt";
        if (!File.Exists(path))
        {
            return "";
        }
        //Read the text from directly from the test.txt file

        StreamReader reader = new StreamReader(path);

        String s = reader.ReadToEnd();

        reader.Close();
        //File.Delete(path);
        return s;
    }

}
