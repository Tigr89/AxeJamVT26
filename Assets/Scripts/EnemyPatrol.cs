using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] Pathfinder pathfinder;
    [SerializeField] Tilemap groundTilemap;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] float speed = 2f;
    [SerializeField] float waitTime = 5f;

    List<Vector2> path;
    int pathIndex;

    void Start()
    {
        StartCoroutine(PatrolRoutine());
    }

    void Update()
    {
        FollowPath();
    }

    IEnumerator PatrolRoutine()
    {
        while (true)
        {
            Vector2 destination = GetRandomWalkablePosition();
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

    Vector2 GetRandomWalkablePosition()
    {
        BoundsInt bounds = groundTilemap.cellBounds;

        for (int i = 0; i < 50; i++)
        {
            int x = Random.Range(bounds.xMin, bounds.xMax);
            int y = Random.Range(bounds.yMin, bounds.yMax);
            Vector3Int cellPos = new Vector3Int(x, y, 0);

            if (!groundTilemap.HasTile(cellPos)) continue;

            Vector2 worldPos = groundTilemap.GetCellCenterWorld(cellPos);

            if (!Physics2D.OverlapBox(worldPos, Vector2.one * 0.9f, 0f, obstacleLayer))
                return worldPos;
        }

        return transform.position;
    }
}
