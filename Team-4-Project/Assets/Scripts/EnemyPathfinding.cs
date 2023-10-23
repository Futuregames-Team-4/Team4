using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    public GameObject player; // Reference to the player object
    public float moveSpeed = 3.0f; // Enemy movement speed
    public GameObject[] tiles; // List of tile game objects

    private Vector3 targetPosition; // Target tile position
    public bool isMoving = false; // Flag to indicate if the enemy is moving

    void CheckTileWalkability(GameObject tileObject)
    {
        Tile tile = tileObject.GetComponent<Tile>();
        if (tile != null)
        {
            if (tile.isWalkable)
            {
                // Tile is walkable
                // Implement pathfinding logic here
                MoveToPlayer();
            }
            else
            {
                // Tile is blocked
                // Handle blocked tile (e.g., avoid it or find an alternative path)
            }
        }
    }

    private void Start()
    {
        // Initially, the enemy is not moving
        isMoving = false;
    }

    private Vector3 CalculateTargetTilePosition(Vector3 playerPosition)
    {
        // Replace this logic with your own algorithm for converting the player's position to a target tile position
        // For example, you can use grid coordinates or other calculations based on your game's grid system.

        // In this example, we'll assume that each tile has a size of 1 unit, and we round the player's position to the nearest tile.
        Vector3 targetTilePosition = new Vector3(Mathf.Round(playerPosition.x), Mathf.Round(playerPosition.y), Mathf.Round(playerPosition.z));

        return targetTilePosition;
    }

    private void MoveToPlayer()
    {
        List<Vector3> path = CalculatePathToPlayer(transform.position, targetPosition);
        Debug.Log("Path calculated. Path count: " + path.Count);
        StartCoroutine(MoveAlongPath(path));
    }

    private List<Vector3> CalculatePathToPlayer(Vector3 start, Vector3 target)
    {
        // Replace this with your pathfinding algorithm to calculate a path from 'start' to 'target'
        // For example, you can use A* or another pathfinding algorithm to determine the path between tiles.

        // In this example, we'll assume a simple path consisting of the start and target positions.
        List<Vector3> path = new List<Vector3>();
        path.Add(start);
        path.Add(target);

        return path;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (!isMoving)
        {
            if (distanceToPlayer < 1.25f)
            {
                targetPosition = CalculateTargetTilePosition(player.transform.position);
                FindAndMoveToPlayer();
            }
        }

        // If the player moves out of a certain range, you can stop the enemy by checking the distance.
        if (isMoving && distanceToPlayer > 2.5f)
        {
            // Stop enemy movement by calling a method to reset the state.
           // StopMoving();
        }
    }
    private void FindAndMoveToPlayer()
    {
        GameObject startTile = GetNearestTile(transform.position);
        GameObject targetTile = GetNearestTile(targetPosition);

        if (startTile != null && targetTile != null)
        {
            CheckTileWalkability(startTile); // Call CheckTileWalkability for the start tile
            CheckTileWalkability(targetTile);
            List<Vector3> path = CalculatePath(startTile, targetTile); // Update this line
            StartCoroutine(MoveAlongPath(path));
        }
    }

    private GameObject GetNearestTile(Vector3 position)
    {
        float minDistance = float.MaxValue;
        GameObject nearestTile = null;

        foreach (var tile in tiles)
        {
            float distance = Vector3.Distance(tile.transform.position, position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestTile = tile;
            }
        }

        return nearestTile;
    }

    private List<Vector3> CalculatePath(GameObject startTile, GameObject targetTile)
    {
        Dictionary<GameObject, float> distances = new Dictionary<GameObject, float>();
        Dictionary<GameObject, GameObject> cameFrom = new Dictionary<GameObject, GameObject>();
        List<GameObject> unvisitedTiles = new List<GameObject>(tiles);

        foreach (var tile in tiles)
        {
            distances[tile] = float.MaxValue;
        }

        distances[startTile] = 0;

        while (unvisitedTiles.Count > 0)
        {
            GameObject currentTile = null;
            float minDistance = float.MaxValue;
            foreach (var tile in unvisitedTiles)
            {
                if (distances[tile] < minDistance)
                {
                    minDistance = distances[tile];
                    currentTile = tile;
                }
            }

            if (currentTile == targetTile)
            {
                // Reconstruct the path
                List<Vector3> path = new List<Vector3>();
                GameObject tile = targetTile;
                while (tile != startTile)
                {
                    path.Insert(0, tile.transform.position);
                    tile = cameFrom[tile];
                }
                path.Insert(0, startTile.transform.position);

                return path;
            }

            unvisitedTiles.Remove(currentTile);

            foreach (var neighbor in GetNeighborTiles(currentTile))
            {
                float tentativeDistance = distances[currentTile] + Vector3.Distance(currentTile.transform.position, neighbor.transform.position);
                if (tentativeDistance < distances[neighbor])
                {
                    distances[neighbor] = tentativeDistance;
                    cameFrom[neighbor] = currentTile;
                }
            }
        }

        // No valid path found
        return new List<Vector3>();
    }
    private List<GameObject> GetNeighborTiles(GameObject tile)
    {
        List<GameObject> neighbors = new List<GameObject>();

        foreach (var potentialNeighbor in tiles)
        {
            if (Vector3.Distance(tile.transform.position, potentialNeighbor.transform.position) <= 1.1f && tile != potentialNeighbor)
            {
                neighbors.Add(potentialNeighbor);
            }
        }

        return neighbors;
    }

    private IEnumerator MoveAlongPath(List<Vector3> path)
    {
        isMoving = true;

        foreach (var waypoint in path)
        {

            Debug.Log("Moving to waypoint: " + waypoint);
            while (transform.position != waypoint)
            {
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
                yield return null;
            }
        }
        isMoving = false;

        Debug.Log("Movement complete.");
    }
}
