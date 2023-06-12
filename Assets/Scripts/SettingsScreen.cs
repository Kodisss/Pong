using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using TMPro;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdownMenu;

    private void Start()
    {
        dropdownMenu.value = PlayerPrefs.GetInt("Difficulty");
    }

    public void SetDifficulty(int input)
    {
        // Save the difficulty setting
        PlayerPrefs.SetInt("Difficulty", input);
        PlayerPrefs.Save();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
