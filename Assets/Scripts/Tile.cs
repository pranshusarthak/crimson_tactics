using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x; 
    public int y; 

    // Method to set the position of the tile in the grid
    public void SetPosition(int x, int y)
    {
        this.x = x; 
        this.y = y; 
    }
}
