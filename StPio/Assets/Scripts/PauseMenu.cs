using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool IsPaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused){
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
        ScrollCounterScript.scrollAmount = 0;
    }

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log(Time.timeScale);
        IsPaused = true;
    }
}
