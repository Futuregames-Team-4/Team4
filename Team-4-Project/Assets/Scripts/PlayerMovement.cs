using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int maxActionPoints = 3; // Numero massimo di ActionPoints che un giocatore può avere.
    private int currentActionPoints; // ActionPoints attuali del giocatore.
    public bool mouseDebug = false; // Variabile per attivare/disattivare il debug del mouse
    public bool useActionPoints = true; // Se true, i movimenti sono vincolati dagli action points.



    private void Start()
    {
        currentActionPoints = maxActionPoints; // Inizializza gli ActionPoints al loro valore massimo all'inizio.
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        int layerMask = 1 << 8; 
        layerMask = ~layerMask;

        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (mouseDebug) // Se mouseDebug è attivo
            {
                // Disegna una linea dalla posizione di origine del ray fino al punto di intersezione con l'oggetto.
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
            }
        }

        if (useActionPoints && currentActionPoints <= 0) return; // Se useActionPoints è true e non ci sono action points, non fare niente.


        if (Input.GetMouseButtonDown(0)) // Left mouse click
        {
            if (hit.collider != null && IsValidMove(hit.collider.transform.position))
            {
                SquareStatus squareStatus = hit.collider.GetComponent<SquareStatus>();
                if (squareStatus != null && !squareStatus.isOccupied)
                {
                    MoveTo(hit.collider.transform.position);
                    if (useActionPoints) 
                    {
                        currentActionPoints--; // Consuma un ActionPoint solo se useActionPoints è true.
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

    // Funzione per gestire il "End Turn".
    public void EndTurn()
    {
        currentActionPoints = maxActionPoints; // Resetta gli ActionPoints al loro valore massimo.
        Debug.Log("Action Points reset to: " + currentActionPoints);
    }
}
