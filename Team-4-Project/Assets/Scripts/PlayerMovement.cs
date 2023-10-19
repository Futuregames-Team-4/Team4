using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            int layerMask = 1 << 8; // Cast rays only against colliders in layer 8.
            layerMask = ~layerMask; // Collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.

            if (Physics.Raycast(ray, out hit))
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    if (IsValidMove(hit.collider.transform.position))
                    {
                        SquareStatus squareStatus = hit.collider.GetComponent<SquareStatus>();
                        if (squareStatus != null && !squareStatus.isOccupied)
                        {
                            MoveTo(hit.collider.transform.position);
                        }
                    }
                }
            }
        }
    }

    private bool IsValidMove(Vector3 targetPosition)
    {
        float xDifference = Mathf.Abs(targetPosition.x - transform.position.x);
        float zDifference = Mathf.Abs(targetPosition.z - transform.position.z);

        return (xDifference == 1.25f && zDifference == 0f) || (zDifference == 1.25f && xDifference == 0f);
    }


    private void MoveTo(Vector3 targetPosition)
    {
        Occupier occupier = GetComponent<Occupier>();
        if (occupier)
        {
            occupier.MoveTo(targetPosition);
        }
    }

}