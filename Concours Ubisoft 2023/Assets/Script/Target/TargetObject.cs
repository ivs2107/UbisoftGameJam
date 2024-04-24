using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TargetObject : MonoBehaviour
{
    //public TileBase tileBase;
    public LevelManager levelManager;
    // public GameObject Turret;
    private int index = 0;
    private GameObject oldObject;

    public  GameObject weapon;
    // Start is called before the first frame update
    void Awake()
    {

        //levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CreateTile()
    {
        if(oldObject != null)
        {
            Destroy(oldObject);
        }
        GameObject parent = GameObject.Find("OutsideObjects");
        if (parent == null)
        {
            parent = GameObject.Find("OutsideObjects(Clone)");
        }

        oldObject = Instantiate(ObjectManager.instance.currentObject, this.transform.position, /*Quaternion.identity*/(this.transform.parent.lossyScale.x < 0 ? Quaternion.Euler(0, 180, 0)  : Quaternion.Euler(0, 0, 0)), parent.transform);
        
        /*index++;
        if(index == items.Length)
        {
            index = 0;
        }*/
        //levelManager.CreateTile(this.transform.position);
    }

    public void CreateTileSelected()
    {
        //float rot_z = Mathf.Atan2(this.transform.position.y, this.transform.position.x) * Mathf.Rad2Deg;
        //weapon.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
       // weapon.transform.LookAt(this.transform.,);
        /*if (levelManager == null)
        {
            levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        }
        levelManager.CreateTileSelected(this.transform.position);*/
    }
}
