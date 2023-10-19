using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = 1 << 8;
            // This would cast rays only against colliders in layer 8.
            // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
            layerMask = ~layerMask;

            if (Physics.Raycast(ray, out hit))
            {

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) // Remember the tag
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