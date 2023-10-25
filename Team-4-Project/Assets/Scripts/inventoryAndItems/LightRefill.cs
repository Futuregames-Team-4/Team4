using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRefill : MonoBehaviour
{

    [SerializeField]
    GameObject objectToInactivate;

    private void OnTriggerEnter(Collider other)
    {
        FuelConsumption fuel = other.transform.parent.GetComponent<FuelConsumption>();
        if (other.CompareTag("Player"))
        {
            Debug.Log("PICK UP MF");
            //collision.gameObject.GetComponent<FuelConsumption>().UseConsumable();
            
            fuel.UseConsumable();

                    objectToInactivate.SetActive(false);
               
            
        }
    }
}
