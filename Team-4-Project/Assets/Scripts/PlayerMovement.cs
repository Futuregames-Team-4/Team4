using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {

                if (hit.collider.CompareTag("Square")) // Remember the tag
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

    private void MoveTo(Vector3 targetPosition)
    {
        Occupier occupier = GetComponent<Occupier>();
        if (occupier)
        {
            occupier.MoveTo(targetPosition);
        }
    }
}