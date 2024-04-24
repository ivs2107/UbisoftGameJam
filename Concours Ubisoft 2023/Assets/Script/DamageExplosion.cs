using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DamageExplosion : MonoBehaviour
{
    public GameObject camera;
    public int damage;
    private LevelManager levelManager;
    private Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        tilemap = levelManager.tilemapWall.GetComponent<Tilemap>();
    }
    private void Awake()
    {
        StartCoroutine(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().Shake(0.5f, 1f));
        Destroy(this.transform.parent.gameObject, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<HealthComponent>().changeHP(-damage);
            //collision.gameObject.GetComponent<EnemyManager>().doDamage(damage);
        }
        if (collision.gameObject.name != "Tilemap_wall")
        {
            //enlever ça pour rebond
            return;
        }
      
        Vector3 hitPosition = Vector3.zero;
        //  if (tilemap != null && tilemapGameObject == collision.gameObject)
       
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                Vector3Int posTile = tilemap.WorldToCell(hitPosition);
                levelManager.UpdateTile(posTile, damage);
            }
        
    }

    
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<HealthComponent>().changeHP(-damage);
            //collision.gameObject.GetComponent<EnemyManager>().doDamage(damage);
        }
       
    }*/
}
