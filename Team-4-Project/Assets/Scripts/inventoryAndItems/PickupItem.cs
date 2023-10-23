using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PickupItems : MonoBehaviour
{
    public Inventory inventory;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out InstanceItemContainer foundItem))
        {
            inventory.AddItem(foundItem.TakeItem());
        }
    }
}


/*public class PickupItem : MonoBehaviour
{
    private Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectsWithTag("Player").GetComponent<Inventory>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    inventory.isFull[i] = true;
                    break;
                }
            }
        }
    }
}*/
