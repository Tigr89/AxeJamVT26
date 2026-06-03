using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] float cellSize = 1f;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] int maxNodes = 500;

    class Node
    {
        public Vector2Int pos;
        public float gCost, hCost;
        public Node parent;
        public float FCost => gCost + hCost;

        public Node(Vector2Int pos) => this.pos = pos;
    }

    public List<Vector2> FindPath(Vector2 startWorld, Vector2 endWorld)
    {
        Vector2Int start = WorldToGrid(startWorld);
        Vector2Int end = WorldToGrid(endWorld);

        var open = new List<Node>();
        var closed = new HashSet<Vector2Int>();
        var nodeMap = new Dictionary<Vector2Int, Node>();

        var startNode = new Node(start) { gCost = 0, hCost = Heuristic(start, end) };
        open.Add(startNode);
        nodeMap[start] = startNode;

        int iterations = 0;
        while (open.Count > 0)
        {
            if (++iterations > maxNodes) return null;

            Node current = open[0];
            for (int i = 1; i < open.Count; i++)
                if (open[i].FCost < current.FCost) current = open[i];

            if (current.pos == end)
                return ReconstructPath(current);

            open.Remove(current);
            closed.Add(current.pos);

            foreach (Vector2Int neighborPos in GetNeighbors(current.pos))
            {
                if (closed.Contains(neighborPos) || IsBlocked(neighborPos)) continue;

                float newG = current.gCost + 1f;

                if (!nodeMap.TryGetValue(neighborPos, out Node neighbor))
                {
                    neighbor = new Node(neighborPos);
                    nodeMap[neighborPos] = neighbor;
                    neighbor.gCost = float.MaxValue;
                }

                if (newG < neighbor.gCost)
                {
                    neighbor.gCost = newG;
                    neighbor.hCost = Heuristic(neighborPos, end);
                    neighbor.parent = current;
                    if (!open.Contains(neighbor))
                        open.Add(neighbor);
                }
            }
        }

        return null;
    }

    static List<Vector2> ReconstructPath(Node end)
    {
        var path = new List<Vector2>();
        for (Node n = end; n != null; n = n.parent)
            path.Add(new Vector2(n.pos.x, n.pos.y));
        path.Reverse();
        return path;
    }

    static IEnumerable<Vector2Int> GetNeighbors(Vector2Int pos)
    {
        yield return pos + Vector2Int.up;
        yield return pos + Vector2Int.down;
        yield return pos + Vector2Int.left;
        yield return pos + Vector2Int.right;
    }

    bool IsBlocked(Vector2Int gridPos)
    {
        Vector2 worldPos = GridToWorld(gridPos);
        return Physics2D.OverlapBox(worldPos, Vector2.one * cellSize * 0.9f, 0f, obstacleLayer);
    }

    static float Heuristic(Vector2Int a, Vector2Int b) =>
        Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);

    Vector2Int WorldToGrid(Vector2 worldPos) =>
        new Vector2Int(
            Mathf.RoundToInt(worldPos.x / cellSize),
            Mathf.RoundToInt(worldPos.y / cellSize)
        );

    Vector2 GridToWorld(Vector2Int gridPos) =>
        new Vector2(gridPos.x * cellSize, gridPos.y * cellSize);
}
