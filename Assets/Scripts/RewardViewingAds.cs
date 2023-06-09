using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewardViewingAds : MonoBehaviour
{
    private const string SaveNumberOfCoin = "_saveNumberOfCoin";
    [SerializeField] private TextMeshProUGUI _textCoins;
    [SerializeField] private GameObject _buttonShowAdv;
    private int _rewardViewingAds = 50;
    private int _numberOfCoins;

    private void Start()
    {
        if (PlayerPrefs.HasKey(SaveNumberOfCoin))
        {
            _numberOfCoins = PlayerPrefs.GetInt(SaveNumberOfCoin);
        }
    }

    public void ShowAdvButton()
    {
        Agava.YandexGames.VideoAd.Show();
        _numberOfCoins += _rewardViewingAds;
        PlayerPrefs.SetInt(SaveNumberOfCoin, _numberOfCoins);
        _textCoins.text = _numberOfCoins.ToString();
        _buttonShowAdv.SetActive(false);
        PlayerPrefs.Save();
    }
}
