
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Agava.YandexGames;
using Agava.YandexGames.Samples;


public class MenuNextLevel : MonoBehaviour
{

    [SerializeField] private GameObject test;
    [SerializeField] TextMeshProUGUI _levelText;
    [SerializeField] Progress _progress;
    [SerializeField] CoinManager _coinManager;

    private void Start()
    {
        _levelText.text = SceneManager.GetActiveScene().name;
    }

    public void OnTest()
    {
        test.SetActive(true);
    }

    public void Restart()
    {
     //   _coinManager.NumberOfCoins += 10;
        _coinManager.SaveToProgress();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("_currentLevel", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("_saveNumberOfCoin", _coinManager.NumberOfCoins += 10);
        if (PlayerAccount.IsAuthorized)
        {
           Leaderboard.SetScore("Coins", _coinManager.NumberOfCoins += 10);      
        }

        PlayerPrefs.Save();
        Progress.Instance.Save();
    }

    public void NextLevel()
    {
            int next = SceneManager.GetActiveScene().buildIndex + 1;      
         //   _coinManager.NumberOfCoins += 100;
            _coinManager.SaveToProgress();
            SceneManager.LoadScene(next);
            PlayerPrefs.SetInt("_currentLevel", next);
            PlayerPrefs.SetInt("_saveNumberOfCoin", _coinManager.NumberOfCoins += 100);

        if (PlayerAccount.IsAuthorized)
        {
            Leaderboard.SetScore("Coins", _coinManager.NumberOfCoins += 100);
        }

        PlayerPrefs.Save();
            Progress.Instance.PlayerInfo.Level = SceneManager.GetActiveScene().buildIndex;
            Progress.Instance.Save();
            if (next == 5 || next == 10 || next == 15|| next == 20)
            {
                Agava.YandexGames.InterstitialAd.Show();
            }
    }

    public void Menu()
    {
        SceneManager.LoadScene(1);
    }

}
