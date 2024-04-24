using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class Resize : MonoBehaviour
{
    // Start is called before the first frame update
    public Tilemap tilemapHot;
    public Tilemap tilemapCold;
    public Tilemap tilemapBase;
    public Tilemap tilemapBorder;
    public Tilemap tilemapWall;
    public Tilemap tilemapTarget;
    private void Awake()
    {
        tilemapHot.CompressBounds();
        tilemapCold.CompressBounds();
        tilemapBase.CompressBounds();
        tilemapBorder.CompressBounds();
        tilemapWall.CompressBounds();
        tilemapTarget.CompressBounds();
    }
}
