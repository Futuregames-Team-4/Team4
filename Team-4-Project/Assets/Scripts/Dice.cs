using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public void DiceRoll()
    {
        int dice = Random.Range(1, 7);

        if (dice == 1)
        {
            //Lose all fuel
            GetComponent<FuelConsumption>().KilledByEnemy();
        }

        if (dice >= 4)
        {
            //Lose some fuel
            GetComponent<FuelConsumption>().HitByEnemy();
        }

        if (dice == 5)
        {
            //Flee to nearby tile
        }

        else
        {
            //Stun enemy
        }
    }
}
