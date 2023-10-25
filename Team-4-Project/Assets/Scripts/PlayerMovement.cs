using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int maxActionPoints = 3;
    public static int currentActionPoints;
    public bool mouseDebug = false;             // Show Raycast of the mouse
    public bool useActionPoints = true;         // Enable or disale ActionPoints
    private RaycastHit hitInfo;
    private GridSystem gridSystem;

    private void Start()
    {
        gridSystem = FindObjectOfType<GridSystem>();
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
        if (Input.GetMouseButtonDown(0) && hitInfo.collider != null && 
        IsValidMove(hitInfo.collider.transform.position) && currentActionPoints > 0)
        {
            SquareStatus squareStatus = hitInfo.collider.GetComponent<SquareStatus>();
            if (squareStatus != null && !squareStatus.isOccupied)
            {
                MoveTo(hitInfo.collider.transform.position);
                ConsumeActionPoint();
            }
        }
    }

    private bool IsValidMove(Vector3 targetPosition)
    {
        float distanceBetweenCells = gridSystem.cellSize + gridSystem.spacing;

        float xDifference = Mathf.Abs(targetPosition.x - transform.position.x);
        float zDifference = Mathf.Abs(targetPosition.z - transform.position.z);

        // Controlla se la mossa è valida
        bool isAdjacentMove = (xDifference == distanceBetweenCells && zDifference == 0f)
                              || (zDifference == distanceBetweenCells && xDifference == 0f);

        if (!isAdjacentMove)
            return false;

        // Verify if the target tile isOccupied
        RaycastHit hit;
        if (Physics.Raycast(targetPosition + Vector3.up * 5f, Vector3.down, out hit)) // Raycast towards target tile
        {
            if (hit.collider.CompareTag("Square"))
            {
                SquareStatus squareStatus = hit.collider.GetComponent<SquareStatus>();
                if (squareStatus && squareStatus.isOccupied)
                    return false; // isOccupied = true
            }
        }
        return true; // isOccupied = false
    }

    private void MoveTo(Vector3 targetPosition)
    {
        Occupier occupier = GetComponent<Occupier>();
        if (occupier)
        {
            occupier.MoveToSquare(targetPosition);
        }
        // Update GridStatus
        Vector2Int previousPos = gridSystem.GetGridPosition(transform.position);
        Vector2Int newPos = gridSystem.GetGridPosition(targetPosition);

    }

    private void ConsumeActionPoint()   // Decrease actionPoints
    {
        if (useActionPoints)
        {
            currentActionPoints--;

            if (currentActionPoints <= 0)
            {
                GameStateManager.Instance.EndPlayerTurn();
            }
        }
    }
}
