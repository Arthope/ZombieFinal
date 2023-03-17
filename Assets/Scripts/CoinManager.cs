using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public int NumberOfCoins;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] GameObject _batton;
    int _saveNumberOfCoin;

    private void Start()
    {
        if (PlayerPrefs.HasKey("_saveNumberOfCoin"))
        {

            NumberOfCoins = PlayerPrefs.GetInt("_saveNumberOfCoin");
        }
        _text.text = NumberOfCoins.ToString();
        transform.parent = null;

    }

    private void Update()
    {
      
        _text.text = NumberOfCoins.ToString();
    }

    public void SaveToProgress()
    {
        Progress.Instance.PlayerInfo.Coins = NumberOfCoins;
    }

    public void ShowAdvButton()
    {
        Agava.YandexGames.VideoAd.Show();
        PlayerPrefs.SetInt("_saveNumberOfCoin", NumberOfCoins += 50);
        _batton.SetActive(false);
    }

    public void AddCoins(int value)
    {
        NumberOfCoins += value;
        PlayerPrefs.SetInt("_saveNumberOfCoin", NumberOfCoins);
        PlayerPrefs.Save();
    }
}
