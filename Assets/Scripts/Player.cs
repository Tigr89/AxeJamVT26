using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Pathfinder pathfinder;
    [SerializeField] float speed = 3f;

    List<Vector2> path;
    int pathIndex;
    Vector2 lastTargetPos;

    void Start() => RecalculatePath();

    void Update()
    {
        if (target == null || pathfinder == null) return;

        if (Vector2.Distance((Vector2)target.position, lastTargetPos) > 0.5f)
            RecalculatePath();

        FollowPath();
    }

    void RecalculatePath()
    {
        if (target == null) return;

        lastTargetPos = target.position;
        path = pathfinder.FindPath(transform.position, target.position);
        if (path != null) path.Add(target.position);
        pathIndex = 0;
    }

    public bool ReachedTarget => target == null || path == null || pathIndex >= path.Count;

    void FollowPath()
    {
        if (CombatScript.main.inCombat != false) return;
        if (path == null || pathIndex >= path.Count) return;

        transform.position = Vector2.MoveTowards(transform.position, path[pathIndex], speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, path[pathIndex]) < 0.05f)
            pathIndex++;
    }
    public void NewTarget(Transform t_pos)
    {
        target = t_pos;
        RecalculatePath();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            CombatScript.main.AddTarget(other.gameObject);
        }
    }
}
