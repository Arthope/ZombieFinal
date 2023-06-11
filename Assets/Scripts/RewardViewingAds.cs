using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewardViewingAds : MonoBehaviour
{
    [SerializeField] private GameObject _buttonShowAdv;
    [SerializeField] private Shop _shop;
    private int _rewardViewingAds = 50;
    private int _numberOfCoins;

    public void ShowAdvButton()
    {
        Agava.YandexGames.VideoAd.Show();
        _buttonShowAdv.SetActive(false);
        _shop.ReceivingAward(_rewardViewingAds);
    }
}
