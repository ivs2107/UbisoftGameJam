using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Tilemap tilemapWall;
    public Tilemap tilemapTarget;
    public GameObject particleEffect;
    public Dictionary<Vector3Int, TileGameObject> dicTiles = new Dictionary<Vector3Int, TileGameObject>();
    public TileBase tileBase;
    public Tilemap tilemapHot;
    public Tilemap tilemapCold;
    public Tilemap tilemapBase;
    public Tilemap tilemapBorder;


    public ScoreManager scoreManager;


    public GameObject HeartGameObject;

    void Start()
    {
        //association de chaque tile à une classe pour pouvoir fair edes actions dessus
        foreach (var pos in tilemapWall.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 place = tilemapWall.CellToWorld(localPlace);
            if (tilemapWall.HasTile(localPlace))
            {
                //tileWorldLocations.Add(place);
                TileGameObject tgo = new TileGameObject(30, localPlace);
                try
                {
                    dicTiles.Add(localPlace, tgo);
                }
                catch (System.Exception)
                {

                  
                }
               
            }
        }
    }


    public void CreateTileSelected(Vector3 posObject)
    {
        tilemapTarget.ClearAllTiles();
        Vector3Int pos = tilemapTarget.WorldToCell(posObject);

        Tile t = Tile.CreateInstance<Tile>();
        Sprite sprite = Resources.Load<Sprite>("tilesetImage/selection_block");
        t.sprite = sprite;
        tilemapTarget.SetTile(pos, t);


    }

    public void CreateTile(Vector3 posObject)
    {
        Vector3Int pos = tilemapWall.WorldToCell(posObject);

        if (tilemapWall.GetTile(pos) == null)
        {
            tilemapWall.SetTile(pos, tileBase);
        }
        TileGameObject tgo = new TileGameObject(30, pos);
        dicTiles.Add(pos, tgo);
        //particle of destruction
        Instantiate(particleEffect, pos, Quaternion.identity);
    }


    public void UpdateTile(Vector3Int posTile, int damage)
    {
       


        if (!dicTiles.ContainsKey(posTile))
        {
            return;
        }
        TileGameObject tgo = dicTiles[posTile];
        TileGameObject.StateBlock sb = tgo.RemoveLifePoints(damage);

        //camera
        // StartCoroutine(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().Shake(0.1f, 0.2f));
        StartCoroutine(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().ProcessShake(1f, 0.1f));





        TileBase tile = tilemapWall.GetTile(posTile);
        if(tile == null)
        {
            Debug.Log("c'est nulllll");
        }
        string nameSprite = tile.name;
        string[] names = nameSprite.Split('_');

        //particle of destruction
        Instantiate(particleEffect, tgo.GetPos(), Quaternion.identity);

        if (sb == TileGameObject.StateBlock.Null)
        {
            //score
            scoreManager.AddScore(ScoreManager.scores.scoreDestructionBlock, tgo.GetPos());


            tilemapWall.SetTile(tgo.GetPos(), null);
            dicTiles.Remove(posTile);
            if (names[0] == "Gaziniere")
            {
                for (int i = 0; i < 20; i++)
                {
                    tilemapHot.SetTile(new Vector3Int(tgo.GetPos().x, tgo.GetPos().y + i, 0), null);
                }

            }
            else if (names[0] == "Frigo")
            {
                for (int i = 0; i < 20; i++)
                {
                    tilemapCold.SetTile(new Vector3Int(tgo.GetPos().x+i, tgo.GetPos().y, 0), null);
                    tilemapCold.SetTile(new Vector3Int(tgo.GetPos().x - i, tgo.GetPos().y, 0), null);


                  

                }
                TileBase tileWall = tilemapWall.GetTile(new Vector3Int(tgo.GetPos().x, tgo.GetPos().y + 1, 0));
                if (tileWall != null && tileWall.name.Contains("Frigo"))
                {
                    UpdateTile(new Vector3Int(tgo.GetPos().x, tgo.GetPos().y + 1, 0), 30);
                }
                TileBase tileWall2 = tilemapWall.GetTile(new Vector3Int(tgo.GetPos().x, tgo.GetPos().y - 1, 0));
                if (tileWall2 != null &&  tileWall2.name.Contains("Frigo"))
                {
                    UpdateTile(new Vector3Int(tgo.GetPos().x, tgo.GetPos().y - 1, 0), 30);
                }
            }
            AudioManager.instance.Play("WallDestroy");
            int rand = Random.Range(0, 10);
            if (rand == 1)
            {
                GameObject parent = GameObject.Find("OutsideObjects");
                if (parent == null)
                {
                    parent = GameObject.Find("OutsideObjects(Clone)");
                }
                Instantiate(HeartGameObject, new Vector3(tgo.GetPos().x+0.5f, tgo.GetPos().y+0.5f, 0),Quaternion.identity, parent.transform);
            }
            return;
        }

        AudioManager.instance.Play("WallHurt");

        int version = (int)sb + 1;

        Tile t = Tile.CreateInstance<Tile>();

 
        Sprite[] sprites = Resources.LoadAll<Sprite>("decor/" + names[0] + "_v" + version);
        if(sprites.Length == 1)
        {
        Sprite sprite = sprites[0];//Resources.Load<Sprite>("decor/" + names[0] + "_v" + version);
            t.sprite = sprite;
            t.name = names[0] + "_v" + version;
        }
        else
        {
            int idSprite = int.Parse(names[2]);
            t.sprite = sprites[idSprite];
            t.name = names[0] + "_v" + version + "_" + names[2];
        }
           
      
        tilemapWall.SetTile(tgo.GetPos(), t);

    }

    public void SetTilemapWall(Vector3Int localPlace,TileBase tile)
    {
        tilemapWall.SetTile(localPlace, tile);
        TileGameObject tgo = new TileGameObject(30, localPlace);
        dicTiles.Add(localPlace, tgo);
    }

    public void DeleteTileMapWall(Vector3Int localPlace)
    {
        tilemapWall.SetTile(localPlace, null);
        dicTiles.Remove(localPlace);
    }

    public void SetTilemapBase(Vector3Int localPlace, TileBase tile)
    {
        tilemapBase.SetTile(localPlace, tile);

    }
    public void SetTilemapHot(Vector3Int localPlace, TileBase tile)
    {
        tilemapHot.SetTile(localPlace, tile);

    }
    public void SetTilemapCold(Vector3Int localPlace, TileBase tile)
    {
        tilemapCold.SetTile(localPlace, tile);

    }

    public void SetTilemapBoder(Vector3Int localPlace, TileBase tile)
    {
        tilemapBorder.SetTile(localPlace, tile);

    }

    public void SetTilemap(Tilemap tilemap ,Vector3Int localPlace, TileBase tile)
    {
        tilemap.SetTile(localPlace, tile);
        if(tilemap == tilemapWall)
        {
            TileGameObject tgo = new TileGameObject(30, localPlace);
            dicTiles.Add(localPlace, tgo);
        }

    }

    public void DeleteTileMapWall()
    {
        dicTiles.Clear();
    }





}
