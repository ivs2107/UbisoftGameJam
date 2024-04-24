using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotManager : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    public float gravityHot;
    void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (rb==null)
        {
            rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        }
        if(collision.tag == "Player")
        {
            collision.GetComponent<CharacterDash>().canDash = true;
            // rb.gravityScale = gravityHot;
            //collision.attachedRigidbody.velocity = new Vector2(collision.attachedRigidbody.velocity.x,15);
            if (collision.attachedRigidbody.velocity.y <15)
            {
                rb.AddForce(new Vector2(0f, 3.00f), ForceMode2D.Impulse);
            }
            /*if (collision.attachedRigidbody.angularVelocity  <=0.05)
                rb.AddForce(new Vector2(0f, 3.00f),ForceMode2D.Impulse);*/
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //collision.GetComponent<CharacterDash>().canDash = false;
            //  rb.gravityScale =1;
        }
    }
}

