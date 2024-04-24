using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationController : MonoBehaviour
{
    //creation with movement
    public Transform targetSelected;
    public TargetObject targetObject;
    public float distance = 3f;
    private Vector2 lastVector = new Vector2(1, 0);
    public float timeInterval = 3;
    public SpriteRenderer IllusatrationPlacement;
    // Start is called before the first frame update

    private float startTime;
    private bool canCall = true;




    void Start()
    {
        //targetSelected = GameObject.Find("Tracking");
        startTime = 0;
    }
    public void Move(Vector2 vector)
    {
        //verify if is null to get last or not 
        if (vector != Vector2.zero)
        {
            lastVector.x = 0;
            lastVector.y = 0;
            if (Mathf.Abs(vector.x) + 0.1 > Mathf.Abs(vector.y))
            {
                if (vector.x > 0)
                {
                    lastVector.x = 1;
                }
                else
                {
                    lastVector.x = -1;
                }
            }
            else
            {
                /* else
                 {
                     lastVector.x = 0;
                 }*/

                if (vector.y > 0)
                {
                    lastVector.y = 1;
                }
                else
                {
                    lastVector.y = -1;
                }
            }
        }
        //move position of the target with the position
        targetSelected.position = new Vector3(lastVector.x * distance, lastVector.y * distance, 0);
        targetSelected.position += this.transform.position;
        targetObject.CreateTileSelected();


    }
    private void Update()
    {

        if (startTime >= timeInterval)
        {
            startTime = 0;
            canCall = true;
            if (IllusatrationPlacement != null)
            {
                IllusatrationPlacement.color = Color.white;
            }
        }
        if (!canCall)
        {
            if (IllusatrationPlacement != null)
            {
                IllusatrationPlacement.color = Color.gray;
            }
            startTime += Time.deltaTime;
        }

    }

    public void Create()
    {
        if (canCall)
        {
            targetObject.CreateTile();
            canCall = false;
        }
    }
}
