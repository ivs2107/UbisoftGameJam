using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGameObject //: MonoBehaviour
{
    // Start is called before the first frame update
    private int lifePointsMax;
    private int lifePoints;
    private Vector3Int pos;

    public enum StateBlock{
        New,
        Damaged,
        Broken,
        Null
    }

    public TileGameObject(int lifePoints,Vector3Int pos)
    {
        this.lifePointsMax = lifePoints;
        this.lifePoints = lifePointsMax;
        this.pos = pos;
    }
    public int GetLifePoints()
    {
        return lifePoints;
    }
    public Vector3Int GetPos()
    {
        return pos;
    }

    public StateBlock RemoveLifePoints(int damage)
    {
        lifePoints -= damage;
        StateBlock sb = StateBlock.New;
        if (lifePointsMax / 3 * 2 >= lifePoints)
        {
            sb = StateBlock.Damaged;
        }
        if (lifePointsMax / 3 >= lifePoints)
        {
            sb = StateBlock.Broken;
        }
       
        if(lifePoints <= 0)
        {
            sb = StateBlock.Null;
        }
        return sb;
    }

}
