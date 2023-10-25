using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject overviewCamera;

    [SerializeField]
    GameObject mapOverview;

    [SerializeField]
    GameObject mainCamera;

    [SerializeField]
    GameObject pauseText;

    [SerializeField]
    GameObject controlsText;

    bool isPaused = false;

    private void Awake()
    {
        pauseMenu.SetActive(false);
        controlsText.SetActive(false);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        
        if (Input.GetKey(KeyCode.Tab))
        {
            ToggleOverviewMap();
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            RemoveOverviewMap();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseMenu.SetActive(isPaused);
    }

    public void ControlsButton()
    {
        pauseText.SetActive(false);
        controlsText.SetActive(true);
    }

    public void BackButton()
    {
        pauseText.SetActive(true);
        controlsText.SetActive(false);
    }

    public void MainMenuButton()
    {
        TogglePause();
        SceneManager.LoadScene("Main Menu");
    }

    void ToggleOverviewMap()
    {
        overviewCamera.SetActive(true);
        mapOverview.SetActive(true);
        mainCamera.SetActive(false);
    }

    void RemoveOverviewMap()
    {
        overviewCamera.SetActive(false);
        mapOverview.SetActive(false);
        mainCamera.SetActive(true);
    }
}
