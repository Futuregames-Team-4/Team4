using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject mainCamera;

    [SerializeField]
    GameObject overviewCamera;


    private void Awake()
    {
        mainCamera.SetActive(true);
        overviewCamera.SetActive(false);
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game.");
        Application.Quit();
    }

    public void StartGame()
    {
        Debug.Log("Load Board1");
        //SceneManager.LoadScene("Board1");
    }
}
