using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTransfo : MonoBehaviour
{

    public GameObject Character0G;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(changeGravityPlayer(collision));
        }
    }
    IEnumerator changeGravityPlayer(Collider2D col)
    {
        GameObject gc = GameObject.FindGameObjectWithTag("GameManager");
        GameObject gp = GameObject.FindGameObjectWithTag("Player");
        GameObject transformation = GameObject.FindGameObjectWithTag("Transformation");
        transformation.transform.position = new Vector3(gp.transform.position.x, gp.transform.position.y - 1, gp.transform.position.z);
        transformation.GetComponent<ParticleSystem>().Play();
        gp.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(1.0f);
        GameObject goInstantiate = Character0G;
        GameObject gnew = Instantiate(goInstantiate, gc.transform.position, gc.transform.rotation);
        gnew.transform.GetChild(0).position = gp.transform.position;
        gnew.transform.GetChild(0).rotation = gp.transform.rotation;
        GameObject.FindGameObjectWithTag("MainCinemachine").GetComponent<CinemachineVirtualCamera>().Follow = gnew.transform.GetChild(0);
        Destroy(gc);
        Destroy(this.gameObject);
    }
}
