using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int maxActionPoints = 3;
    private int currentActionPoints;
    public bool mouseDebug = false;             // Show Raycast of the mouse
    public bool useActionPoints = true;         // Enable or disale ActionPoints
    private GameStateManager gameStateManager;  // Game Manager
    private RaycastHit hitInfo;

    private void Start()
    {
        currentActionPoints = maxActionPoints;
    }

    private void Update()
    {
        HandleMouseRaycast();
        HandleMouseInput();
    }

    private void HandleMouseRaycast()      // Raycast of the mouse
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask) && mouseDebug)
        {
            Debug.DrawRay(ray.origin, ray.direction * hitInfo.distance, Color.red);
        }
    }

    private void HandleMouseInput()         // Input of the mouse
    {
        if (useActionPoints && currentActionPoints <= 0) return;
        if (Input.GetMouseButtonDown(0) && IsValidMove(hitInfo.collider.transform.position))
        {
            SquareStatus squareStatus = hitInfo.collider.GetComponent<SquareStatus>();
            if (squareStatus != null && !squareStatus.isOccupied)
            {
                MoveTo(hitInfo.collider.transform.position);
                ConsumeActionPoint();
            }
        }
    }

    private bool IsValidMove(Vector3 targetPosition)    // Moves up or down, not diagonally
    {
        float xDifference = Mathf.Abs(targetPosition.x - transform.position.x);
        float zDifference = Mathf.Abs(targetPosition.z - transform.position.z);
        return (xDifference == 1.25f && zDifference == 0f) || (zDifference == 1.25f && xDifference == 0f);
    }

    private void MoveTo(Vector3 targetPosition)         // Move the object
    {
        Occupier occupier = GetComponent<Occupier>();
        if (occupier)
        {
            occupier.MoveToSquare(targetPosition);
        }
    }

    private void ConsumeActionPoint()   // Decrease action Points
    {
        if (useActionPoints)
        {
            currentActionPoints--;
        }
    }

    public void EndTurn()               // End Player Turn
    {
        currentActionPoints = maxActionPoints;
    }
}
