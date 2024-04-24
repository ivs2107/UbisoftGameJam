using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttack : MonoBehaviour
{
    public GameObject projectile;
    public Transform currentProjectile;
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = currentProjectile.position;
        InvokeRepeating("throwBaguette", 0f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void throwBaguette()
    {
        Instantiate(projectile, pos, this.transform.rotation);
    }
}
