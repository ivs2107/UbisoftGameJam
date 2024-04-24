using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform firePoint;
    //public GameObject bulletPrefab;

    public float distance = 3f;
    private Vector2 lastVector = new Vector2(1, 0);

    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

    }*/

    private float randy = 0.2f;
    /* private float startTime;
     private bool canCall = true;

     private void Start()
     {
         startTime = 0;
     }

     private void Update()
     {
         if (startTime >= PlayerAttack.instance.attackSpeed)
         {
             startTime = 0;
             canCall = true;
         }
         if (!canCall)
         {

             startTime += Time.deltaTime;
         }
     }*/

    public void Shoot()
    {
        float angle = Mathf.Atan2(lastVector.y, lastVector.x) * Mathf.Rad2Deg;

        for (int j = 0; j < PlayerAttack.instance.bulletsAngleBonus; j++)
        {
            int angleBonus = 15;
            if (j == 0)
            {
                angleBonus = 0;
            }
            else if (j % 2 == 0)
            {
                angleBonus = -angleBonus * (j - 1);
            }
            else
            {
                angleBonus = angleBonus * j;
            }
            Quaternion rotation = Quaternion.Euler(0, 0, angle + angleBonus);
            for (int i = 0; i < PlayerAttack.instance.numberBullets; i++)
            {
                if (PlayerAttack.instance.bullet.transform.localScale.x > 1)
                {
                    randy = 0;
                }
                Instantiate(PlayerAttack.instance.bullet, new Vector3(firePoint.position.x + Random.Range(-0.2f, 0.2f), firePoint.position.y + Random.Range(-randy, randy), firePoint.position.z), rotation);

            }
        }
        AudioManager.instance.Play("Shoot");


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
            /*else
            {
                lastVector.y = 0;
            }*/
        }
        //move position of the target with the position
        firePoint.position = new Vector3(lastVector.x * distance, lastVector.y * distance, 0);
        firePoint.position += this.transform.position;
        //targetSelected.GetComponentInChildren<TargetObject>().CreateTileSelected();


    }
}
