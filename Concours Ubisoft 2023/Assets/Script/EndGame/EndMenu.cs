using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject parent_rectangle;
    public TMP_InputField username;
    public TMP_Text score;
    public TMP_Text scoreTime;
    public TMP_Text bonusTimeText;
    public ServerManager serverManager;
    public GameObject PanelUser;
    public GameObject PanelLeaderBoard;
    public TMP_Text leaderboardText;


    private int index = 0;



    private float startTime;
    private bool canCall = true;


    private void Start()
    {
       // username.interactable = false;
        username.Select();
        username.ActivateInputField();
        startTime = 0;
       // username.text = "   ";
    }

    private void Awake()
    {
        
        try
        {
            ScoreManager scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
            score.text = scoreManager.getScore().ToString();
           // scoreTime.text = scoreManager.getTimeScore().Item2.ToString();
            StartCoroutine(scoreManager.countDown(scoreManager.getScore(),0, score));
            //StartCoroutine(scoreManager.countDown(scoreManager.getTimeScore().Item2, 0, scoreTime));
            //bonusTimeText.text = string.Format("Bonus Time \nx {0:0.00}", scoreManager.getTimeScore().Item1);

        }
        catch (System.Exception)
        {

            
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Cursor.visible =false;
        username.Select();
        username.ActivateInputField();

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

    public void UpdateRectangles ()
    {
       // AudioManager.instance.Play("Select");
        Debug.Log(username.text.Length);
        Debug.Log(parent_rectangle.transform.childCount);
        if (username.text.Length <= parent_rectangle.transform.childCount)
        {
            parent_rectangle.transform.GetChild(username.text.Length).GetComponent<Animator>().enabled = true;
            parent_rectangle.transform.GetChild(username.text.Length).GetComponent<Animator>().SetBool("stopAnimation", false);
        }
        if (username.text.Length < parent_rectangle.transform.childCount && username.text.Length != 0)
        {
            //parent_rectangle.transform.GetChild(username.text.Length + 1).GetComponent<Animator>().enabled = false;
            parent_rectangle.transform.GetChild(username.text.Length-1).GetComponent<Animator>().SetBool("stopAnimation",true);
            parent_rectangle.transform.GetChild(username.text.Length-1).GetComponent<Animator>().Play("no_animation", 0);
        }
        /*if (username.text.Length -1 >= 2)
        {
            //parent_rectangle.transform.GetChild(username.text.Length - 1).GetComponent<Animator>().enabled = false;
            parent_rectangle.transform.GetChild(username.text.Length -1 ).GetComponent<Animator>().SetBool("stopAnimation", true);
            parent_rectangle.transform.GetChild(username.text.Length -1).GetComponent<Animator>().Play("no_animation", 0);
        }*/
    }

    public void callServer()
    {
        if(username.text.Length >0)
        {
            PanelUser.SetActive(false);

            PanelLeaderBoard.SetActive(true);
            try 
            {
                ServerManager.sendScore(username.text, score.text);
                
                Player[] players = serverManager.getLeaderBoard();
                for (int i = 0; i < players.Length; i++)
                {
                    leaderboardText.text += string.Format("{0,2}. {1} : {2,10}  ({3})\n", (i + 1), players[i].pseudo, players[i].score, players[i].date);
                }
            }
            catch(WebException ex)
            {
                string s = ReadString();
                var v = s.Split(" ");
                if (s.Length==0 ||int.Parse(v[1]) < int.Parse(score.text))
                {
                    WriteString(username.text + " " + score.text);
                }
            }
           
           // serverManager.
        }
    }

    public void quitButton()
    {
        Application.Quit();
    }

    public void restartButton()
    {
        if (AudioManager.instance != null)
        {
            GameObject go = AudioManager.instance.gameObject;
            foreach (var root in go.scene.GetRootGameObjects())
                Destroy(root);
        }
        SceneManager.LoadScene(0);
    }



    private char character = 'A';


    public void MoveCursor(Vector2 vector)
    {
        if (canCall && PanelUser.gameObject.activeSelf)
        {
            if (vector.y < -0.9f)
            {
                //listButtons[index].GetComponentInChildren<TMP_Text>().color = Color.white;
               /* index++;
                if (index == listButtons.Count)
                {
                    index = 0;
                }*/
                //listButtons[index].GetComponentInChildren<TMP_Text>().color = Color.yellow;
                
                AudioManager.instance.Play("Select");
                canCall = false;
                if (username.text.Length -1 == index)
                    username.text = username.text.Remove(index, 1);
                username.text = username.text.Insert(index,character.ToString());
                character++;
                if(character > 'Z')
                {
                    character = 'A';
                }
            }
            else if (vector.y > 0.9f)
            {
                /*listButtons[index].GetComponentInChildren<TMP_Text>().color = Color.white;
                if (index == 0)
                {
                    index = listButtons.Count;
                }
                index--;

                listButtons[index].GetComponentInChildren<TMP_Text>().color = Color.yellow;*/
                AudioManager.instance.Play("Select");
                canCall = false;
                if (username.text.Length != 0)
                    username.text = username.text.Remove(index, 1);
                username.text = username.text.Insert(index, character.ToString());
                character--;
                if (character < 'A')
                {
                    character = 'Z';
                }
            }
        }

    }

    public void ActivateButton()
    {

        if  (PanelUser.gameObject.activeSelf)
        {
            AudioManager.instance.Play("SelectPlay");
            if (index == 2)
            {
                callServer();
            }
            else
            {
               
                    index++;
                    parent_rectangle.transform.GetChild(username.text.Length).GetComponent<Animator>().enabled = true;
                    parent_rectangle.transform.GetChild(username.text.Length).GetComponent<Animator>().SetBool("stopAnimation", false);
                    if (index != 0)
                    {
                    parent_rectangle.transform.GetChild(username.text.Length - 1).GetComponent<Animator>().SetBool("stopAnimation", true);
                    parent_rectangle.transform.GetChild(username.text.Length - 1).GetComponent<Animator>().Play("no_animation", 0);
                    }
                    username.text = username.text + " ";
                    character = 'A';
            }


        }

       
    }

    public void cancelAction()
    {
        if (PanelUser.gameObject.activeSelf)
        {
            if (index == 0)
                return;

            parent_rectangle.transform.GetChild(username.text.Length - 2).GetComponent<Animator>().enabled = true;
            parent_rectangle.transform.GetChild(username.text.Length - 2).GetComponent<Animator>().SetBool("stopAnimation", false);
            parent_rectangle.transform.GetChild(username.text.Length - 1).GetComponent<Animator>().SetBool("stopAnimation", true);
            parent_rectangle.transform.GetChild(username.text.Length - 1).GetComponent<Animator>().Play("no_animation", 0);
            index--;
            character = 'A';
            username.text = username.text.Remove(index, 1);
        }

    }



    static void WriteString(string t)

    {

        string path = "Assets/Resources/score.txt";
        File.Delete(path);

        //Write some text to the test.txt file

        StreamWriter writer = new StreamWriter(path, true);

        writer.WriteLine(t);

        writer.Close();


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
