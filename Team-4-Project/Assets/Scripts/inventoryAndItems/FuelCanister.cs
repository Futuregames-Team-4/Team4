using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCanister : MonoBehaviour
{
    public void FuelRefill()
    {
        gameObject.GetComponent<FuelConsumption>().UseConsumable();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // the object identified by hit.transform was clicked
                // do whatever you want
            }
        }
    }
}
