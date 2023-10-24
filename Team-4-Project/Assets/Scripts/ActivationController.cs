using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationController : MonoBehaviour
{
    private GameStateManager gameStateManager;  // Game Manager
    
    [SerializeField]
    GameObject objectToActivate;

    public NewEnemyPathfinding scriptComponent;

    void Awake()
    {
        scriptComponent = gameObject.GetComponent<NewEnemyPathfinding>();
        objectToActivate.SetActive(false);
        // If the GameObject with this script is different from objectToActivate, 
        // then you can also set the scriptComponent.enabled to false here if you want.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objectToActivate.SetActive(true);
            scriptComponent.enabled = true; // Enables the NewEnemyPathfinding script
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objectToActivate.SetActive(false);
            scriptComponent.enabled = false; // Disables the NewEnemyPathfinding script
        }
    }
}
