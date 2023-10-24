using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int maxActionPoints = 3;
    public static int currentActionPoints;
    public bool mouseDebug = false;             // Show Raycast of the mouse
    public bool useActionPoints = true;         // Enable or disale ActionPoints
    private GameStateManager gameStateManager;  // Game Manager
    private RaycastHit hitInfo;
    private GridSystem gridSystem;
    private Vector2Int playerGridPosition;

    private void Start()
    {
        gridSystem = FindObjectOfType<GridSystem>();
        currentActionPoints = maxActionPoints;
        GameStateManager.Instance.StartPlayerTurn(); // End the enemy's turn
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
        if (Input.GetMouseButtonDown(0) && hitInfo.collider != null && IsValidMove(hitInfo.collider.transform.position))
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

        // Verifica se la casella target è occupata
        RaycastHit hit;
        if (Physics.Raycast(targetPosition + Vector3.up * 5f, Vector3.down, out hit)) // Proietta un ray verso il basso dalla posizione target
        {
            if (hit.collider.CompareTag("Square"))
            {
                SquareStatus squareStatus = hit.collider.GetComponent<SquareStatus>();
                if (squareStatus && squareStatus.isOccupied)
                    return false; // Casella occupata
            }
        }

        return true; // Casella libera
    }

    private void MoveTo(Vector3 targetPosition)
    {
        Occupier occupier = GetComponent<Occupier>();
        if (occupier)
        {
            occupier.MoveToSquare(targetPosition);
        }

        // Aggiorna lo stato della griglia
        Vector2Int previousPos = gridSystem.GetGridPosition(transform.position);
        Vector2Int newPos = gridSystem.GetGridPosition(targetPosition);

        //Debug.Log(gridSystem.GetGridPosition(transform.position));

    }

    private void ConsumeActionPoint()   // Decrease action Points
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


    public void EndTurn()               // End Player Turn
    {
        currentActionPoints = maxActionPoints;
    }
    public Vector2Int GetPlayerGridPosition()
    {
            return playerGridPosition = gridSystem.GetGridPosition(transform.position);
    }
}
