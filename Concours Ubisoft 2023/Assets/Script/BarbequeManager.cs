using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbequeManager : MonoBehaviour
{
    public GameObject bulletInstance;
    public GameObject particleEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            //this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            //this.GetComponent<Rigidbody2D>().velocity = collision.gameObject.transform.right * 20;
            this.gameObject.layer = LayerMask.NameToLayer("Item");
            for (int i = 0; i < 10; i++)
            {
                float x = Random.Range(this.transform.position.x - 1, this.transform.position.x + 1);
                float y = Random.Range(this.transform.position.y - 1, this.transform.position.y + 1);
                Vector3 pos = new Vector3(x, y, this.transform.position.z);
                Instantiate(bulletInstance, pos,collision.transform.rotation);
            }
            /*
            Instantiate(particleEffect, this.transform.position, Quaternion.identity);*/
            Destroy(this.gameObject);
        }
    }
}
