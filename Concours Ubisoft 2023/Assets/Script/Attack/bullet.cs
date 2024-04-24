using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class bullet : MonoBehaviour
{

    private float speed = 15f;
    public Rigidbody2D rb;
    private float damage = 10;
    //private Tilemap tilemap;
    private LevelManager levelManager;
    private Tilemap tilemap;
    public string tagToAttack = "Enemy";

    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.tag != "Enemy")
        {
            speed = PlayerAttack.instance.bulletSpeed;
            damage = PlayerAttack.instance.bulletDamage;
        }


        if (rb != null)
        {
            rb.velocity = this.transform.right * speed;
        }
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        tilemap = levelManager.tilemapWall.GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == tagToAttack)
        {
            collision.gameObject.GetComponent<HealthComponent>().changeHP(-(damage));
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == tagToAttack)
        {
            if(tagToAttack != "Player")
                collision.gameObject.GetComponent<HealthComponent>().changeHP(-damage);
            Destroy(this.gameObject);
        }
        if (collision.gameObject.name != "Tilemap_wall")
        {
            //enlever ça pour rebond
            Destroy(this.gameObject);
            return;
        }
        //Destroy(collision.gameObject);
        //Vector3Int wallPos = tilemap.WorldToCell(collision.transform.position);
        //tilemap.SetTile(wallPos, null);

        Vector3 hitPosition = Vector3.zero;
        //  if (tilemap != null && tilemapGameObject == collision.gameObject)

        foreach (ContactPoint2D hit in collision.contacts)
        {
            hitPosition.x = hit.point.x - 0.1f * hit.normal.x;
            hitPosition.y = hit.point.y - 0.1f * hit.normal.y;
            Vector3Int posTile = tilemap.WorldToCell(hitPosition);
            levelManager.UpdateTile(posTile, (int)damage);
            break;
        }
       
        Destroy(this.gameObject);
    }

}
