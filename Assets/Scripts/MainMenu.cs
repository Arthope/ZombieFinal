using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    int _currentLevel;
    public int next;

    public void NextLevel()
    {
        next = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(next);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void MainMenuLevel()
    {
        if (PlayerPrefs.HasKey("_currentLevel"))
        {
            int level = PlayerPrefs.GetInt("_currentLevel");
            SceneManager.LoadScene(level);
        }
        else
        {
            next = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(next);
        }
    }
}
