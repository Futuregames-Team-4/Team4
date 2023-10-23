using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelConsumption : MonoBehaviour
{
    [SerializeField]
    RectTransform fuelLeft;

    public int fuel;

    private void Update()
    {
        switch (fuel)
        {
            case 0:
                vectorSize(100, 0);
                break;
            case 1:
                vectorSize(100, 30);
                break;
            case 2:
                vectorSize(100, 60);
                break;
            case 3:
                vectorSize(100, 90);
                break;
            case 4:
                vectorSize(100, 120);
                break;
            case 5:
                vectorSize(100, 150);
                break;
            case 6:
                vectorSize(100, 180);
                break;
            case 7:
                vectorSize(100, 210);
                break;
            case 8:
                vectorSize(100, 240);
                break;
            case 9:
                vectorSize(100, 270);
                break;
            case 10:
                vectorSize(100, 300);
                break;
        }
    }

    private void vectorSize(int x, int y)
    {
        fuelLeft.sizeDelta = new Vector2(x, y);
    }

    public void MovementCost()
    {
        fuel--;
    }

    public void UseConsumable()
    {
        fuel = 10;
    }

    public void HitByEnemy()
    {
        fuel = fuel - 5;
    }
}
