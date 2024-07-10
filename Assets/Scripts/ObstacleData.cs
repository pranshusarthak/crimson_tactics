using UnityEngine;

// Attribute to create a new menu item in Unity's Create menu to create an instance of this ScriptableObject
[CreateAssetMenu(fileName = "ObstacleData", menuName = "ScriptableObjects/ObstacleData", order = 1)]
public class ObstacleData : ScriptableObject
{
    // Boolean array for the obstacle grid. Each element represents whether a tile has an obstacle or not.
    // The grid is  be 10x10, so the array has 100 elements.
    public bool[] obstacleGrid = new bool[100];
}
