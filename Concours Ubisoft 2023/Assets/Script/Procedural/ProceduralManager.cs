using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class ProceduralManager : MonoBehaviour
{
    public List<GameObject> Levels;
    public List<GameObject> LevelsTopDown;
    public List<GameObject> LevelsNormal;
    private List<GameObject> LevelsNormalCopy;
    private List<GameObject> LevelsTopDownCopy;
    public GameObject LevelManager;
    private int lastRand;

    private int UpEnnemiesRoom = 4;

    [FormerlySerializedAs("oldEnemies")] public Transform oldOutsideObjects;
    public Vector3 offset;


    private Dictionary<String, Tilemap> tilemaps;

    private void Start()
    {
        tilemaps = new Dictionary<string, Tilemap>();
        tilemaps.Add("Tilemap_wall", LevelManager.GetComponent<LevelManager>().tilemapWall);
        tilemaps.Add("Tilemap_bordure", LevelManager.GetComponent<LevelManager>().tilemapBorder);
        tilemaps.Add("Tilemap_base", LevelManager.GetComponent<LevelManager>().tilemapBase);
        tilemaps.Add("Tilemap_hot", LevelManager.GetComponent<LevelManager>().tilemapHot);
        tilemaps.Add("Tilemap_cold", LevelManager.GetComponent<LevelManager>().tilemapCold);

        LevelsNormalCopy = new List<GameObject>(LevelsNormal);
        LevelsTopDownCopy = new List<GameObject>(LevelsTopDown);
    }


    public int count = 1;
    public bool Stronger = true;
    // Start is called before the first frame update
    // A rendre singleton et appeler une fonction statique pour rendre le code plus rapide.
    public void load(Vector3 positionTrigger,bool isTopDown)
    {
        /*TODO :               
         *      - Gerer la position
         *      - Gerer les ennemies
         *      - Faire le sas ?
         *      - Le mur invisible
         */
        
        //return;

        if(isTopDown)
        {
            if (LevelsTopDown.Count == 0)
            {
                LevelsTopDown = new List<GameObject>(LevelsTopDownCopy);
            }
            Levels = LevelsTopDown;
        }
        else
        {
            if (LevelsNormal.Count == 0)
            {
                LevelsNormal = new List<GameObject>(LevelsNormalCopy);
            }
            Levels = LevelsNormal;
        }

        if(count % UpEnnemiesRoom == 0)
        {
            PlayerAttack.instance.ennemyDamage += 10;
            PlayerAttack.instance.ennemHP *= 1.5f;
            Stronger = true;
            if (UpEnnemiesRoom != 2)
            {
                UpEnnemiesRoom--;
            }
        }
        else
        {
            Stronger = false;
        }

        

        int rand = lastRand;
        rand = Random.Range(0, Levels.Count);
        lastRand = rand;

        GameObject grid = Levels[rand].transform.Find("Grid").gameObject;
        GameObject outsideObjects = Levels[rand].transform.Find("OutsideObjects").gameObject;
        Levels.RemoveAt(rand);



        Vector3 newPosOutsideObjects = new Vector3(oldOutsideObjects.position.x + this.offset.x+ (int)positionTrigger.x, oldOutsideObjects.position.y + this.offset.y + (int)positionTrigger.y, oldOutsideObjects.position.z + this.offset.z + (int)positionTrigger.z);
        


        GameObject newOutsideObjects = Instantiate(outsideObjects, newPosOutsideObjects, quaternion.identity);

        Tilemap[] tileMapsTab = new Tilemap[grid.transform.childCount];
        
        // tab of string with the tilemaps
        for (int i = 0; i < grid.transform.childCount; i++)
        {
            tileMapsTab[i] = grid.transform.Find(grid.transform.GetChild(i).name).GetComponent<Tilemap>();
        }

        foreach (var tileMap in tileMapsTab)
        {

            String name = tileMap.transform.GetComponent<Tilemap>().name;
            if (name == "Tilemap_selection")
            {
                continue;
            }

            tilemaps[name].ClearAllTiles();
            if (name == "Tilemap_wall")
            {
                LevelManager.GetComponent<LevelManager>().DeleteTileMapWall();
            }
            foreach (var pos in tileMap.cellBounds.allPositionsWithin)
            {
                Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                LevelManager.GetComponent<LevelManager>().SetTilemap(
                        tilemaps[name],
                        new Vector3Int(localPlace.x + (int)offset.x * count, localPlace.y, localPlace.z),
                        tileMap.GetTile(localPlace));
            }
            tilemaps[name].CompressBounds();
        }

            /*
            switch (name)
            {
                // Wall
                
                case "Tilemap_wall" : 
                    // TODO : je ne sais pas si c'est mieux de mettre la boucle ici ou en dehors du switch
                    //LevelManager.GetComponent<LevelManager>().tilemapWall.transform.TransformPoint(tileMap.localBounds.min);
                    

                    LevelManager.GetComponent<LevelManager>().tilemapWall.ClearAllTiles();
                    LevelManager.GetComponent<LevelManager>().DeleteTileMapWall();
                    foreach (var pos in tileMap.cellBounds.allPositionsWithin)
                    {
                        Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                        LevelManager.GetComponent<LevelManager>().SetTilemapWall(
                                new Vector3Int(localPlace.x + (int)offset.x * count + (int)positionTrigger.x, localPlace.y + (int)positionTrigger.y, localPlace.z + (int)positionTrigger.z),

                                tileMap.GetTile(localPlace));
                    }
                    LevelManager.GetComponent<LevelManager>().tilemapWall.CompressBounds();
                    break;
                
                // Border
                case "Tilemap_bordure" :
                    LevelManager.GetComponent<LevelManager>().tilemapBorder.ClearAllTiles();
                    //tileMap.boun
                    foreach (var pos in tileMap.cellBounds.allPositionsWithin)
                    {
                        Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                        LevelManager.GetComponent<LevelManager>().SetTilemapBoder(
                           new Vector3Int(
                           localPlace.x + (int)offset.x * count + (int)positionTrigger.x ,
                           localPlace.y + (int)positionTrigger.y,
                           localPlace.z + (int)positionTrigger.z),

                            tileMap.GetTile(localPlace));
                    }
                    LevelManager.GetComponent<LevelManager>().tilemapBorder.CompressBounds();

                    break;
                
                // Base
                case "Tilemap_base" :
                    LevelManager.GetComponent<LevelManager>().tilemapBase.ClearAllTiles();
                    foreach (var pos in tileMap.cellBounds.allPositionsWithin)
                    {
                        Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                        LevelManager.GetComponent<LevelManager>().SetTilemapBase(
                            new Vector3Int(localPlace.x + (int)offset.x * count + (int)positionTrigger.x, localPlace.y + (int)positionTrigger.y, localPlace.z + (int)positionTrigger.z),

                            tileMap.GetTile(localPlace));
                    }
                    LevelManager.GetComponent<LevelManager>().tilemapBase.CompressBounds();

                    break;
                
                // Hot
                case "Tilemap_hot" :
                    LevelManager.GetComponent<LevelManager>().tilemapHot.ClearAllTiles();
                    foreach (var pos in tileMap.cellBounds.allPositionsWithin)
                    {
                        Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                        LevelManager.GetComponent<LevelManager>().SetTilemapHot(
                            new Vector3Int(localPlace.x + (int)offset.x * count + (int)positionTrigger.x, localPlace.y + (int)positionTrigger.y, localPlace.z + (int)positionTrigger.z),

                            tileMap.GetTile(localPlace));
                    }
                    LevelManager.GetComponent<LevelManager>().tilemapHot.CompressBounds();

                    break;
                
                // Cold
                case "Tilemap_cold" :
                    LevelManager.GetComponent<LevelManager>().tilemapCold.ClearAllTiles();
                    foreach (var pos in tileMap.cellBounds.allPositionsWithin)
                    {
                        Vector3Int localPlace = new Vector3Int(pos.x , pos.y, pos.z );

                        LevelManager.GetComponent<LevelManager>().SetTilemapCold(
                            new Vector3Int(localPlace.x + (int)offset.x * count + (int)positionTrigger.x, localPlace.y + (int)positionTrigger.y, localPlace.z + (int)positionTrigger.z),
                            tileMap.GetTile(localPlace));
                    }
                    LevelManager.GetComponent<LevelManager>().tilemapCold.CompressBounds();

                    break;
                
                default:
                    break;
            }
        }*/

        count++;
        Destroy(oldOutsideObjects.gameObject);
        oldOutsideObjects = newOutsideObjects.transform;

        StartCoroutine(ScanNewPath());





    }

    IEnumerator ScanNewPath()
    {
        yield return new WaitForSeconds(0.5f);
        

        AstarPath.active.astarData.gridGraph.center = AstarPath.active.astarData.gridGraph.center + offset;
        //AstarPath.active.UpdateGraphs(LevelManager.GetComponent<LevelManager>().tilemapBorder.localBounds);
        AstarPath.active.Scan();

        GameManager.instance.setEnemiesList();
    }

}
