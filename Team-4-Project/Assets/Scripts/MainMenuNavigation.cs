using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuNavigation : MonoBehaviour
{
    [SerializeField]
    GameObject titleScreen;

    //[SerializeField]
    //GameObject optionsScreen;

    [SerializeField]
    GameObject creditsScreen;


    private void Awake()
    {
        MainMenuButton();
    }

    public void MainMenuButton()
    {
        titleScreen.SetActive(true);
        creditsScreen.SetActive(false);
    }

    public void OptionsButton()
    {
        Debug.Log("Opening Options");
    }

    public void CreditsButton()
    {
        titleScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }
}
