using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    // Start is called before the first frame update
    public int time = 3;
    void Start()
    {
        Destroy(this.gameObject, time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
