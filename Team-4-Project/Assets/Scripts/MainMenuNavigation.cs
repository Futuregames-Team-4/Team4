using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNavigation : MonoBehaviour
{
    [SerializeField]
    GameObject titleScreen;

    //[SerializeField]
    //GameObject optionsScreen;

    [SerializeField]
    GameObject creditsScreen;

    [SerializeField]
    GameObject controlsScreen;

    private void Awake()
    {
        MainMenuButton();
    }

    public void StartGame()
    {
        Debug.Log("Starting game");
        SceneManager.LoadScene("GeorgiScene");
    }

    public void MainMenuButton()
    {
        titleScreen.SetActive(true);
        creditsScreen.SetActive(false);
        controlsScreen.SetActive(false);
    }

    public void CreditsButton()
    {
        titleScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void ControlsButton()
    {
        titleScreen.SetActive(false);
        controlsScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
