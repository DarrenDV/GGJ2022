using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }

    private void Resume()
    {
        gameUI.SetActive(true);
        pauseMenu.SetActive(false);
        
        Time.timeScale = 1f;
        gamePaused = false;
    }

    private void Pause()
    {
        gameUI.SetActive(false);
        pauseMenu.SetActive(true);
        
        Time.timeScale = 0f;
        gamePaused = true;
    }
}
