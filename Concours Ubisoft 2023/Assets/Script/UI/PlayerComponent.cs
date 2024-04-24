using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerComponent : MonoBehaviour
{
    [SerializeField]
    private HealthComponent playerHealthComponent;

    public float dmg = 10;

    // Start is called before the first frame update
    void Start()
    {
        playerHealthComponent = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<HealthComponent>();
        dmg = PlayerAttack.instance.ennemyDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealthComponent== null)
        {
            playerHealthComponent = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<HealthComponent>();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
           // float dmg = 10;
            playerHealthComponent.changeHP(-dmg);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
           // float dmg = 10;
            playerHealthComponent.changeHP(-dmg);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
          //  float dmg = 10;
            playerHealthComponent.changeHP(-dmg);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
           // float dmg = 10;
            playerHealthComponent.changeHP(-dmg);
        }
    }
    public void OnCharacterDeath()
    {
        SceneManager.LoadScene("ending_Ivan");
    }
}
