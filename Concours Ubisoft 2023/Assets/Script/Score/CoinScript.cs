using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public ScoreManager scoreManager;
    public void Awake()
    {
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            scoreManager.AddScore(ScoreManager.scores.scoreCoin, this.transform.position);
            AudioManager.instance.Play("Coin");
            Destroy(this.gameObject);
        }
    }
}
