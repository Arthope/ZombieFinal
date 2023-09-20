using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Agava.YandexGames;
using Agava.YandexGames.Samples;

public class RewardViewingAds : MonoBehaviour
{
    [SerializeField] private GameObject _buttonShowAdv;
    [SerializeField] private Shop _shop;
    [SerializeField] private SoundMuteHandler _soundMuteHandler;
    [SerializeField] private GameObject _soundButton;
    private int _rewardViewingAds = 50;
    private int _numberOfCoins;

    public void ShowAdvButton()
    {
        Agava.YandexGames.VideoAd.Show(OnOpenVideo, OnRewarded, OnClose);
    }

    private void OnOpenVideo()
    {
        Time.timeScale = 0;
        _soundMuteHandler.OnVideoOpened();
        _soundButton.SetActive(false);

    }

    private void OnRewarded()
    {
        _buttonShowAdv.SetActive(false);
        _shop.ReceivingAward(_rewardViewingAds);
    }

    private void OnClose()
    {
        _soundButton.SetActive(true);
        Time.timeScale = 1;
        _soundMuteHandler.OnVideoClosed();
    }
}
