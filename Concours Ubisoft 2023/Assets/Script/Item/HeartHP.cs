using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartHP : MonoBehaviour
{
    public int valueHPtoAdd;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerAttack.instance.gameObject.GetComponent<HealthComponent>().AddHp(valueHPtoAdd);
            AudioManager.instance.Play("Coin");
            Destroy(this.gameObject);
        }
    }
}
