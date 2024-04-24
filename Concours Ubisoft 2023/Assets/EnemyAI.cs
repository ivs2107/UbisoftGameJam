using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public Transform enemyGFX;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);

    }

    void UpdatePath()
    {
        if(seeker.IsDone() && target != null)
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
            currentWaypoint++;
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (path == null || target == null)
            return;
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized ;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance<nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (rb.velocity.x >= 0.1f && force.x > 0f)
        {
            enemyGFX.localScale = new Vector3(-1f , 1f , 1f);
        }
        else if (rb.velocity.x <= -0.1 && force.x < 0f)
        {
            enemyGFX.localScale = new Vector3(1f , 1f , 1f );
        }
    }

    public void getTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
