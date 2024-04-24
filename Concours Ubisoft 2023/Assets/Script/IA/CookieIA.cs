using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieIA : MonoBehaviour
{
    public int speed;
    public Rigidbody2D rb;
    private int direction = 1;
    private int curSpeed;

    private float startTime;
    private bool canCall = true;
    // Start is called before the first frame update
    void Start()
    {
        
        rb.velocity = transform.parent.right * speed;
        rb.inertia = 0;
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.parent.right * curSpeed * direction;
        curSpeed++;
        if (curSpeed > speed)
            curSpeed = speed;

        if (startTime >= 1)
        {
            startTime = 0;
            canCall = true;
        }
        if (!canCall)
        {
            startTime += Time.deltaTime;
        }
    }

    // Update is called once per frame
    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D hit in collision.contacts)
        {
            var hitposition = Vector2.Dot(hit.point.normalized, hit.normal.normalized);
            if (Mathf.Abs(hitposition) < 0.9)
            {
                continue;
            }
            if (canCall/*collision.gameObject.tag == "Wall" *//*&& collision.contacts.*/)
            {
                canCall = false;
                if (direction == 1)
                {
                    //rb.AddForce(,ForceMode2D.)
                    //rb.velocity = -transform.parent.right * speed;
                    //left = false;
                    direction = -1;
                }
                else
                {
                    // rb.velocity = transform.parent.right * speed;
                    // left = true;
                    direction = 1;
                }
                curSpeed = 0;
            }
            break;
        }
        
    }
}
