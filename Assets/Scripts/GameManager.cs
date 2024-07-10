using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance;

    public GameObject playerPrefab; 
    public GameObject enemyPrefab;

    private GameObject playerInstance; 
    private GameObject enemyInstance; 
    private IAI enemyAI; 
    public ObstacleData obstacleData; 
    public float moveSpeed = 2f; 

    private void Awake()
    {
        // Implementing the Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Instantiate the player at the starting position
        playerInstance = Instantiate(playerPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity);

        // Instantiate the enemy at an initial position
        enemyInstance = Instantiate(enemyPrefab, new Vector3(9, 0.5f, 9), Quaternion.identity);

        // Initialize enemy AI
        enemyAI = enemyInstance.GetComponent<IAI>();
        enemyAI.Initialize(playerInstance);
    }

    // Method to move the player to the specified target position
    public void MovePlayerTo(Vector3 targetPosition)
    {
        // Check if the target position is valid 
        if (IsValidTarget(targetPosition))
        {
            // Find a path from the player's current position to the target position
            List<Vector3> path = FindPath(playerInstance.transform.position, targetPosition);
            if (path != null)
            {
                
                StartCoroutine(MovePlayerAlongPath(path));
            }
        }
    }

    
    private bool IsValidTarget(Vector3 targetPosition)
    {
        int index = (int)(targetPosition.z * 10 + targetPosition.x); // Calculate the index in the obstacle grid
        return index >= 0 && index < 100 && !obstacleData.obstacleGrid[index]; // Check if the index is valid and not an obstacle
    }

    // Method to find a path from the start position to the end position
    private List<Vector3> FindPath(Vector3 start, Vector3 end)
    {
        Queue<Vector3> queue = new Queue<Vector3>(); // Queue for breadth-first search
        Dictionary<Vector3, Vector3> cameFrom = new Dictionary<Vector3, Vector3>(); // Dictionary to store the path

        queue.Enqueue(start); // Enqueue the start position
        cameFrom[start] = start; // Mark the start position as visited

        Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right }; // Possible movement directions

        while (queue.Count > 0)
        {
            Vector3 current = queue.Dequeue(); // Dequeue the next position

            // If the current position is the end position, reconstruct the path
            if (current == end)
            {
                List<Vector3> path = new List<Vector3>();
                while (current != start)
                {
                    path.Add(current);
                    current = cameFrom[current];
                }
                path.Reverse();
                return path; // Return the reconstructed path
            }

            // Check each possible direction
            foreach (Vector3 direction in directions)
            {
                Vector3 neighbor = current + direction;
                if (IsValidTarget(neighbor) && !cameFrom.ContainsKey(neighbor))
                {
                    queue.Enqueue(neighbor); // Enqueue the neighbor position
                    cameFrom[neighbor] = current; // Mark the neighbor as visited
                }
            }
        }
        return null; // Return null if no path is found
    }

    // Coroutine to move the player along the path
    private IEnumerator MovePlayerAlongPath(List<Vector3> path)
    {
        // Notify the player controller that the player is moving
        playerInstance.GetComponent<PlayerController>().SetIsMoving(true);

        // Move the player along each waypoint in the path
        foreach (Vector3 waypoint in path)
        {
            while (Vector3.Distance(playerInstance.transform.position, waypoint) > 0.1f)
            {
                playerInstance.transform.position = Vector3.MoveTowards(playerInstance.transform.position, waypoint, moveSpeed * Time.deltaTime);
                yield return null; // Yield here to prevent freezing
            }
        }

        // Snap the player to the final position in the path
        playerInstance.transform.position = path[path.Count - 1];

        // Notify the player controller that the player has finished moving
        playerInstance.GetComponent<PlayerController>().SetIsMoving(false);
    }
}
