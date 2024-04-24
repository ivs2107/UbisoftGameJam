using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int scoreTotal = 0;
    public int CountFPS = 30;
    public float Duration = 1f;
    public TMP_Text scoreText;
    public ChronemetreManager chronemetreManager;
    public float time;
    public int ratio;
    public GameObject prefabScore;
    public GameObject CanvaScore;

    public enum scores
    {
        scoreDestructionBlock = 50,
        scoreKillingEnnemies = 200,
        scoreLoadScene = 1000,
        scoreCoin = 500
    }


    //mettre ici les points
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            time = chronemetreManager.time;
            SceneManager.LoadScene("ending_ivan");

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            time = chronemetreManager.time;
            if (AudioManager.instance != null)
            {
                GameObject go = AudioManager.instance.gameObject;
                foreach (var root in go.scene.GetRootGameObjects())
                    Destroy(root);
            }
            SceneManager.LoadScene(0);


        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public async void AddScore(scores score, Vector3 pos)
    {
        int addScore = 0;
        if (score == scores.scoreLoadScene && chronemetreManager.clearedInTime)
        {
            addScore = (int)score*2;
        }
        else
        {
            addScore = (int)score;
        }
        int newScore = this.scoreTotal + addScore;
        StartCoroutine(countDown(newScore, this.scoreTotal));
        this.scoreTotal = newScore;
        GameObject gm = Instantiate(prefabScore, pos, Quaternion.identity, CanvaScore.transform);
        gm.GetComponent<TMP_Text>().text = (addScore).ToString();
        gm.transform.DOMoveY(pos.y + 2, 1).OnComplete(() => Destroy(gm));

    }


    public IEnumerator countDown(int newScore,int oldScore, TMP_Text tmp_Text=null)
    {
        if(tmp_Text == null)
        {
            tmp_Text = scoreText;
        }
        WaitForSeconds Wait = new WaitForSeconds(1f / CountFPS);
       // float wait = 1f / Time.deltaTime;  //(newScore-oldScore);
        int previousValue = oldScore;
        int stepAmount = Mathf.CeilToInt((newScore - oldScore) / (CountFPS * Duration));

        while (previousValue < newScore)
        {
            previousValue += stepAmount; // (-20 - 0) / (30 * 1) = -0.66667 -> -1              0 + -1 = -1
            if (previousValue > newScore)
            {
                previousValue = newScore;
            }

            tmp_Text.text = String.Format("{0,8:00000000}", previousValue);

            yield return Wait;
        }
    }

   public int getScore()
   {
        return scoreTotal;
   }

    public (float,int) getTimeScore()
    {
        float multiplication = 100/Mathf.Pow(10,Mathf.Log(time)/500);

        return (multiplication,Mathf.RoundToInt(multiplication * scoreTotal));
    }
}
