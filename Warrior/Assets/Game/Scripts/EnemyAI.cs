using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDist = 3;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    public Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        seeker.StartPath(body.position, target.position, pathComplete);
    }

    void pathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
            return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
        }
        else reachedEndOfPath = false;

        Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - body.position).normalized; //normalize will make sure the length of the vector is 1 
        Vector2 force = dir * speed * Time.deltaTime;

        body.AddForce(force);

        float distance = Vector2.Distance(body.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDist)
        {
            currentWaypoint++;
        }
    }
}
