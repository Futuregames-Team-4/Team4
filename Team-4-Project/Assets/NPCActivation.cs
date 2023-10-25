using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCActivation : MonoBehaviour
{
    //private GameStateManager gameStateManager;  // Game Manager

    [SerializeField]
    GameObject objectToActivate;

    void Awake()
    {
        objectToActivate.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objectToActivate.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objectToActivate.SetActive(false);
        }
    }
}
