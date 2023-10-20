using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    bool isPaused = false;

    private void Awake()
    {
        pauseMenu.SetActive(false);
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
