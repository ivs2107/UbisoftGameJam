using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EffectManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //canBeDestroyed = false;
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Temperature" )
        {
            Destroy(this.gameObject);
        }
        if(collision.tag == "Wall")
        {
            Destroy(this.gameObject);
           /* Tilemap tilemap = collision.GetComponent<Tilemap>();
            Vector2 vec2 = collision.ClosestPoint(this.transform.position);
            TileBase tileBase = tilemap.GetTile(new Vector3Int((int)vec2.x, (int)vec2.y, 0));
            string[] names = tileBase.name.Split('_');*/
           /* if (names[1] == "v3")
            {
                Destroy(this.gameObject);
            }*/
        }
        
    }
}
