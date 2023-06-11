using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject middleLine;
    private bool isPaused;

    // Start is called before the first frame update
    private void Start()
    {
        pauseMenu.SetActive(false);
        middleLine.SetActive(true);
        isPaused = false;
    }

    // Update is called once per frame
    private void Update()
    {
        ManageInputs();
    }

    public bool GetPaused()
    {
        return isPaused;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        middleLine.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        middleLine.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void ManageInputs()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (isPaused && Input.GetButtonDown("Cancel"))
        {
            ResumeGame();
        }
    }
}
