using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCanister : MonoBehaviour
{
    public void FuelRefill()
    {
        gameObject.GetComponent<FuelConsumption>().UseConsumable();
    }
        
}
