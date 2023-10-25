using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRefill : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<FuelConsumption>().UseConsumable();
        }
    }
}
