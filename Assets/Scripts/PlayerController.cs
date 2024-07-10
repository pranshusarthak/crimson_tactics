using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera mainCamera; // Reference to the main camera
    private bool isMoving = false; // Flag to track if the player is currently moving

    void Start()
    {
        mainCamera = Camera.main; 
    }

    void Update()
    {
        if (isMoving) return; // If the player is currently moving, do not process input

        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position to detect where the player clicked
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Calculate the target position on the grid based on where the raycast hit
                Vector3 targetPosition = new Vector3(Mathf.RoundToInt(hit.point.x), 0.5f, Mathf.RoundToInt(hit.point.z));
                
                // Move the player to the target position using GameManager
                GameManager.Instance.MovePlayerTo(targetPosition);
            }
        }
    }

  
    public void SetIsMoving(bool moving)
    {
        isMoving = moving; 
    }
}
