using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenMenu : MonoBehaviour
{
    [SerializeField] private GameObject endSound;

    private void Start()
    {
        GameObject SFX = Instantiate(endSound);
        Destroy(SFX,0.5f);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("PongGame");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
