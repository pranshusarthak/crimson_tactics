using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    
    public ObstacleData obstacleData;
    
    
    public GameObject obstaclePrefab;

    
    void Start()
    {
        
        GenerateObstacles();
    }

    // Method to generate obstacles on the grid
    void GenerateObstacles()
    {
        // Loop through each position in the 10x10 grid
        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                // Calculate the index in the obstacleGrid array
                int index = y * 10 + x;

                // Check if the current grid position should have an obstacle
                if (obstacleData.obstacleGrid[index])
                {
                    // Calculate the position for the obstacle
                    Vector3 position = new Vector3(x, 0.5f, y); 
                    
                    // Instantiate the obstacle at the calculated position
                    Instantiate(obstaclePrefab, position, Quaternion.identity);
                }
            }
        }
    }
}
