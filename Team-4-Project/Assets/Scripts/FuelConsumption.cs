using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                SceneManager.LoadScene("GameOverScene");
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
        GetComponent<PlayerMovement>();

        if (PlayerMovement.currentActionPoints == 0)
        {
            MovementCost();
        }
        if (fuel <= 0) 
        {
            fuel = 0;
        }
    }

    private void vectorSize(int x, int y)
    {
        fuelLeft.sizeDelta = new Vector2(x, y);
    }

    public void MovementCost()
    {
        fuel--;
        Debug.Log("Minus 1 fuel");
        PlayerMovement.currentActionPoints = 3;
    }

    public void UseConsumable()
    {
        fuel = 10;
    }

    public void HitByEnemy()
    {
        fuel = fuel - 3;
    }
}
