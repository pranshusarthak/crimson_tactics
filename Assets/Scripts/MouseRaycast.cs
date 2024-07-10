using UnityEngine;
using UnityEngine.UI;

public class MouseRaycast : MonoBehaviour
{
    public Text tileInfoText; 
    public Camera mainCamera; 

    void Update()
    {
        // Create a ray from the camera to the mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform a raycast to check if it hits any object
        if (Physics.Raycast(ray, out hit))
        {
            // Try to get the Tile component from the hit object
            Tile tile = hit.transform.GetComponent<Tile>();

            // If a Tile component is found, display its position in the UI Text element
            if (tile != null)
            {
                tileInfoText.text = $"Tile Position: ({tile.x}, {tile.y})";
            }
        }
        else
        {
            // If no object is hit, clear the UI Text element
            tileInfoText.text = "";
        }
    }
}
