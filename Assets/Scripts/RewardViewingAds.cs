using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewardViewingAds : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textCoins;
    [SerializeField] private GameObject _buttonShowAdv;
    private int _rewardViewingAds = 50;
    private int _numberOfCoins;

    private void Start()
    {
        if (PlayerPrefs.HasKey("_saveNumberOfCoin"))
        {
            _numberOfCoins = PlayerPrefs.GetInt("_saveNumberOfCoin");
        }
    }

    public void ShowAdvButton()
    {
        Agava.YandexGames.VideoAd.Show();
        _numberOfCoins += _rewardViewingAds;
        PlayerPrefs.SetInt("_saveNumberOfCoin", _numberOfCoins);
        _textCoins.text = _numberOfCoins.ToString();
        _buttonShowAdv.SetActive(false);
    }
}
