using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionIA : MonoBehaviour
{
    public EnemyAI enemyAI;
    public GameObject ExclamationPoint;
    private void OnTriggerStay2D(Collider2D collision)
    {
       // RaycastHit2D hit = Physics2D.Raycast(transform.position, collision.transform.position,10,LayerMask.NameToLayer("Enemy"));
       // float distance = Vector3.Distance(collision.transform.position, transform.position);
        if (collision.tag == "Player" /*&&  Mathf.Abs(hit.distance-distance) <= 0.3*/ )
        {
            enemyAI.getTarget();
            ExclamationPoint.SetActive(true);
            Destroy(this);

        }
    }
}
