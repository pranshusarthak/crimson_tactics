using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tilePrefab; 
    public int gridWidth = 10; 
    public int gridHeight = 10; 
    public float tileSpacing = 1.1f; 

    void Start()
    {
        
        GenerateGrid();
    }

    
    void GenerateGrid()
    {
        // Loop through each position in the grid
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                // Calculate the position for each tile
                Vector3 position = new Vector3(x * tileSpacing, 0, y * tileSpacing);

                // Instantiate the tile at the calculated position
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);

                // Set the position of the tile in the Tile script
                tile.GetComponent<Tile>().SetPosition(x, y);
            }
        }
    }
}
