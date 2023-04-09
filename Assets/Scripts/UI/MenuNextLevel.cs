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
    [SerializeField] CoinManager _coinManager;
    private int rewardWinning = 100;
    private int rewardLosing = 10;

    public void OnTest()
    {
        test.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("_currentLevel", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("_saveNumberOfCoin", _coinManager.NumberOfCoins += rewardLosing);
        if (PlayerAccount.IsAuthorized)
        {
           Leaderboard.SetScore("Coins", _coinManager.NumberOfCoins += rewardLosing);      
        }
        PlayerPrefs.Save();
    }

    public void NextLevel()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;      
        SceneManager.LoadScene(next);
        PlayerPrefs.SetInt("_currentLevel", next);
        PlayerPrefs.SetInt("_saveNumberOfCoin", _coinManager.NumberOfCoins += rewardWinning);
        if (PlayerAccount.IsAuthorized)
        {
            Leaderboard.SetScore("Coins", _coinManager.NumberOfCoins += rewardWinning);
        }
        PlayerPrefs.Save();       
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
