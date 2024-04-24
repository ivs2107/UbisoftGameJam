using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public float G = 6.674f;
    public Rigidbody2D rb;


    private void FixedUpdate()
    {
        /*Attractor[] attractors = FindObjectsOfType<Attractor>();
        foreach (Attractor attractor in attractors)
        {
            if()
            Attract(attractor);
        }*/
    }

    void Attract(Rigidbody2D objToAttract)
    {

        Rigidbody2D rbToAttract = objToAttract;//.rb;
        if (rbToAttract.mass > rb.mass)
        {
            return;
        }

        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;
        if(distance <= 1f)
        {
            return;
        }

        float forceMagnitude = G *(rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(force);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D rbToAttract = collision.attachedRigidbody;
        if (rbToAttract != null)
        {
            Attract(rbToAttract);
        }
    }

}
