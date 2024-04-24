using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// manages the health of an entity
/// </summary>
public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnDeathEvent;

    [SerializeField]
    public float maxHp;

    [SerializeField]
    healthBarComponent healthBar;


    [SerializeField]
    private float timeBetweenHits;

    [SerializeField]
    private RedFlashComponent redFlash = null;
    private  float redFlashTime = 0.2f;

    public float hp;
    private float timeSinceLastHit = 0;
    private float initialMaxHp;
    int i = 0;
    void Start()
    {
        if (this.tag == "Enemy")
        {
            maxHp = PlayerAttack.instance.ennemHP;
        }
        else
        {
            maxHp = PlayerAttack.instance.playerMaxHP;
        }
        hp = maxHp;
        initialMaxHp = maxHp;
    }
    private void Update()
    {
        timeSinceLastHit += Time.deltaTime;
    }
    /// <summary>
    /// changes the hp
    /// </summary>
    /// <param name="dmg">the damage dealth, negative value for damages and positive value for heals</param>
    public void changeHP(float hpChange)
    {
        if(timeSinceLastHit > timeBetweenHits)
        {
            AudioManager.instance.Play("Hit");
            hp += hpChange;
            healthBar?.setWidth(hp / maxHp);
            if (hp <= 0)
                OnDeathEvent.Invoke();
            timeSinceLastHit = 0;

            if(hpChange < 0)
            {
                if (redFlash == null)
                {
                    redFlash = GameObject.FindGameObjectWithTag("GameManager").GetComponentInChildren<RedFlashComponent>();
                    redFlashTime = timeBetweenHits;
                }

                StartCoroutine(redFlash.FlashRed(redFlashTime));
            }
        }
    }
    public void ChangeMaxHp(float maxHpChange)
    {
        maxHp += maxHpChange;
        hp = maxHp;
        healthBar.setNewMaxWidth(maxHp / initialMaxHp);
        //healthBar?.setWidth(1);
    }



    public void AddHp(float valueHp)
    {
        if(maxHp> (valueHp+hp))
        {
            hp += valueHp; 
        }
        else
        {
            hp = maxHp;
        }
        healthBar?.setWidth(hp / maxHp);
    }
}
