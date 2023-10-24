using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyPathfinding : MonoBehaviour
{
    public GridSystem gridSystem;
    private List<Vector2Int> path;
    private Vector2Int playerPos;
    public PlayerMovement player;

    public bool shouldFollowPlayer = false;

    void Start()
    {
        path = new List<Vector2Int>();
    }

    void Update()
    {
        if (shouldFollowPlayer)
        {
            Vector2Int playerGridPosition = gridSystem.GetGridPosition(player.transform.position - gridSystem.transform.position);
            FindPathToPlayer(playerGridPosition);
            shouldFollowPlayer = false;  // Reset the value to avoid continuously invoking FindPathToPlayer
        }
    }


    public void FindPathToPlayer(Vector2Int playerPos)
    {
        this.playerPos = playerPos;
        Debug.Log("Player Position" + playerPos);
        Vector2Int startPos = gridSystem.GetGridPosition(transform.position);
        Debug.Log("Posizione di inzio:" + startPos);
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();

        queue.Enqueue(playerPos);
        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();
            if (current == startPos)
            {
                CreatePath(cameFrom);
                return;
            }

            foreach (Vector2Int neighbor in GetNeighbors(current))
            {
                if (!cameFrom.ContainsKey(neighbor))
                {
                    queue.Enqueue(neighbor);
                    cameFrom[neighbor] = current;
                }
            }
        }
    }

    private List<Vector2Int> GetNeighbors(Vector2Int pos)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();
        neighbors.Add(new Vector2Int(pos.x + 1, pos.y));
        neighbors.Add(new Vector2Int(pos.x - 1, pos.y));
        neighbors.Add(new Vector2Int(pos.x, pos.y + 1));
        neighbors.Add(new Vector2Int(pos.x, pos.y - 1));

        return neighbors;
    }

    private void CreatePath(Dictionary<Vector2Int, Vector2Int> cameFrom)
    {
        Vector2Int current = gridSystem.GetGridPosition(transform.position);
        path.Clear();

        while (current != playerPos)
        {
            path.Add(current);
            current = cameFrom[current];
        }

        if (path.Count > 1)
            path.RemoveAt(path.Count - 1);
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        foreach (Vector2Int pos in path)
        {
            // Interpolazione lineare per un movimento più fluido
            Vector3 startPosition = transform.position;
            Vector3 targetPosition = gridSystem.GetWorldPosition(pos);
            float journeyLength = Vector3.Distance(startPosition, targetPosition);
            float startTime = Time.time;
            float distanceCovered = (Time.time - startTime) * journeyLength;
            float fractionOfJourney = distanceCovered / journeyLength;

            while (fractionOfJourney < 1)
            {
                distanceCovered = (Time.time - startTime) * journeyLength;
                fractionOfJourney = distanceCovered / journeyLength;
                transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
                yield return null;
            }
            yield return new WaitForSeconds(0.5f); // puoi cambiare la durata dell'attesa per far muovere il nemico più velocemente o lentamente
        }
    }
}
