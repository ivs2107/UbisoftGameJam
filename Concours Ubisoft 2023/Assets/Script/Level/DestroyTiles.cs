using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestroyTiles : MonoBehaviour
{
    //private Tilemap tilemap;
    private LevelManager levelManager;
    private Tilemap tilemap;
    //public GameObject particleEffect;// a changer l'emplacement mdr
    public int damage = 10;
    // Start is called before the first frame update
    void Start()
    {

        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        tilemap = levelManager.tilemapWall.GetComponent<Tilemap>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name != tilemap.name)
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
            hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
            hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
            Vector3Int posTile = tilemap.WorldToCell(hitPosition);
            levelManager.UpdateTile(posTile, damage);
                
        }
        Destroy(this.gameObject);
    }
}
