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
    private const string SaveNumberOfCoin = "_saveNumberOfCoin";
    private const string CurrentLevel = "_currentLevel";
    private const string LeaderboardName = "Coins";
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private CoinDispley _coinDispley;
    [SerializeField] private Shop _shop;
    [SerializeField] private SoundMuteHandler _soundMuteHandler;
    private int _menuSceneNumber = 1;
    private int _nextLevelNumber;
    private int _rewardWinning = 100;
    private int _rewardLosing = 10;
    private int _firstAdvScene = 5;
    private int _secondAdvScene = 10;
    private int _thirdAdvScene = 15;
    private int _fourthAdvScene = 20;
    private int _currentCountCoinsPlayers = 0;

    private void Start()
    {
        if (PlayerPrefs.HasKey(SaveNumberOfCoin))
        {
            _currentCountCoinsPlayers = PlayerPrefs.GetInt(SaveNumberOfCoin);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt(CurrentLevel, SceneManager.GetActiveScene().buildIndex);
        _shop.ReceivingAward(_rewardLosing);
        if (PlayerAccount.IsAuthorized)
        {
           Leaderboard.SetScore(LeaderboardName, _currentCountCoinsPlayers += _rewardLosing);      
        }
        PlayerPrefs.Save();
    }

    public void NextLevel()
    {
        _nextLevelNumber = SceneManager.GetActiveScene().buildIndex + 1;      
        PlayerPrefs.SetInt(CurrentLevel, _nextLevelNumber);
        _shop.ReceivingAward(_rewardWinning);
        SceneManager.LoadScene(_nextLevelNumber);
        if (PlayerAccount.IsAuthorized)
        {
            Leaderboard.SetScore(LeaderboardName, _currentCountCoinsPlayers += _rewardWinning);
        }
        if (_nextLevelNumber == _firstAdvScene || _nextLevelNumber == _secondAdvScene || _nextLevelNumber == _thirdAdvScene || _nextLevelNumber == _fourthAdvScene)
        {
            ShowAdv();
        }
        PlayerPrefs.Save();       
    }
  
    public void Menu()
    {
        SceneManager.LoadScene(_menuSceneNumber);
    }

    public void ShowAdv()
    {
        Agava.YandexGames.VideoAd.Show(Open, Close);
    }

    private void Open()
    {
        _soundMuteHandler.OnVideoOpened();
    }

    private void Close()
    {
        _soundMuteHandler.OnVideoClosed();
    }

}
