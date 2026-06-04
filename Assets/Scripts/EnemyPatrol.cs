using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] Pathfinder pathfinder;
    [SerializeField] Transform patrolPoints;
    [SerializeField] float speed = 2f;
    [SerializeField] float waitTime = 5f;
    [SerializeField] SpinScript spinScript;
    

    Transform[] waypoints;
    List<Vector2> path;
    int pathIndex;
    int lastWaypointIndex = -1;

    void Awake()
    {
        spinScript = GameObject.Find("SpinLogic").GetComponent<SpinScript>();
        ObjectID = spinScript.AddIcon(gameObject);
        if (patrolPoints == null)
            patrolPoints = GameObject.Find("PatrolPoints").transform;

        int count = patrolPoints.childCount;
        waypoints = new Transform[count];
        for (int i = 0; i < count; i++)
            waypoints[i] = patrolPoints.GetChild(i);

        
    }

    void Start()
    {
        StartCoroutine(PatrolRoutine());
    }

    void Update()
    {
        if (CombatScript.main.inCombat != false) return;
        FollowPath();
    }

    IEnumerator PatrolRoutine()
    {
        while (true)
        {
            Vector2 destination = PickRandomWaypoint();
            path = pathfinder.FindPath(transform.position, destination);
            pathIndex = 0;

            if (path != null)
                yield return new WaitUntil(() => pathIndex >= path.Count);

            yield return new WaitForSeconds(waitTime);
        }
    }

    void FollowPath()
    {
        if (path == null || pathIndex >= path.Count) return;

        transform.position = Vector2.MoveTowards(transform.position, path[pathIndex], speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, path[pathIndex]) < 0.05f)
            pathIndex++;
    }

    Vector2 PickRandomWaypoint()
    {
        if (waypoints.Length == 1) return waypoints[0].position;

        int index;
        do { index = Random.Range(0, waypoints.Length); }
        while (index == lastWaypointIndex);

        lastWaypointIndex = index;
        return waypoints[index].position;
    }
}
