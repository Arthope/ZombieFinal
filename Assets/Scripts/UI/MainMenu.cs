using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private int _currentLevel;
    private int _nextLevel;

    public void NextLevel()
    {
        _nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(_nextLevel);
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
            _nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(_nextLevel);
        }
    }
}
