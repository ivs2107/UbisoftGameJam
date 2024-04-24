using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
   // public int lifePoints = 30;
    public GameObject ParticleEffect;
    public ScoreManager scoreManager;
    public Transform transformObject;
    public void Awake()
    {
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    public void OnDeath()
    {
        if (transformObject == null)
            transformObject = this.transform;
        scoreManager.AddScore(ScoreManager.scores.scoreKillingEnnemies, transformObject.position);
        Instantiate(ParticleEffect, transformObject.position, Quaternion.identity);
        AudioManager.instance.Play("Enemy2");
        Destroy(this.gameObject);
    }

    /*public void doDamage(int damage)
    {
        lifePoints-= damage;
        if (lifePoints <= 0)
        {
            scoreManager.AddScore(ScoreManager.scores.scoreKillingEnnemies,this.transform.position);
            Instantiate(ParticleEffect,this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }*/



}
