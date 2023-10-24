using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionController : MonoBehaviour
{
    FuelConsumption fuel;
    Light lanternLight;
    CapsuleCollider capsuleTrigger;

    private void Awake()
    {
        fuel = GetComponent<FuelConsumption>();
        lanternLight = GetComponentInChildren<Light>();
        capsuleTrigger = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        switch (fuel.fuel)
        {
            case 0: 
                lanternLight.range = 0.5f;
                capsuleTrigger.radius = 0.5f;
                break;
            case 1: 
            case 2:
            case 3:
                lanternLight.range = 3f;
                capsuleTrigger.radius = 2f;
                break;
            case 4:
            case 5:
            case 6:
                lanternLight.range = 5f;
                capsuleTrigger.radius = 3f;
                break;
            case 7:
            case 8:
            case 9:
                lanternLight.range = 8f;
                capsuleTrigger.radius = 4f;
                break;
            case 10:
                lanternLight.range = 10f;
                capsuleTrigger.radius = 5f;
                break;
        }
    }
}
