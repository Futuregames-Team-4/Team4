using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableDisappears : MonoBehaviour
{
    //private GameStateManager gameStateManager;  // Game Manager

    [SerializeField]
    GameObject objectToInactivate;

    void Awake()
    {
        objectToInactivate.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objectToInactivate.SetActive(true);
        }
    }

    
}
