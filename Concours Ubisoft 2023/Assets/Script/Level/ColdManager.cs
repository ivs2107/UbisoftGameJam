using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdManager : MonoBehaviour
{
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Awake()
    {
        //rb2d = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (rb2d == null)
        {
            rb2d = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        }
        if (collision.tag == "Player")
        {
            rb2d.AddForce(new Vector2(0f, -1f), ForceMode2D.Impulse);
        }
    }
}
