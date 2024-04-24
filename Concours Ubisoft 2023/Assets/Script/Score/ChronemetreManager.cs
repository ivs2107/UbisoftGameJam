using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChronemetreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text scoreText;
    public float time;
    public bool clearedInTime = true;
    void Start()
    {
        time = 0f;
        //time = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(!clearedInTime)
        {
            return;
        }
        time -= Time.deltaTime;
        if (time <= 0)
        {
            time = 0;
            clearedInTime = false;

        }
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = time % 60;
        scoreText.text = String.Format("{0,2:00}:{1,5:00.00}", minutes, seconds);
    }
}
