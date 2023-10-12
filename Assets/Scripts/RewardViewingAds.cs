using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Agava.YandexGames;
using Agava.YandexGames.Samples;
using System;
using UnityEngine.UI;

public class RewardViewingAds : MonoBehaviour
{
    [SerializeField] private GameObject _buttonShowAdv;
    [SerializeField] private Shop _shop;
    [SerializeField] private SoundMuteHandler _soundMuteHandler;
    [SerializeField] private GameObject _soundButton;
    [SerializeField] private Button _buttonAd;
    private int _rewardViewingAds = 50;
    private int _numberOfCoins;

    public void ShowAdvButton()
    {
        Agava.YandexGames.VideoAd.Show(OnOpenVideo, OnRewarded, OnClose, OnError);
    }

    private void OnError(string obj)
    {
        _soundMuteHandler.OnVideoOpened();
        Time.timeScale = 0;
        
        Debug.Log("Близко");
        if (Input.GetKeyDown("space"))
        {
            _soundMuteHandler.OnVideoOpened();
            Debug.Log("Cработало");
        }

        if (obj != null)
        {
            _soundMuteHandler.OnVideoOpened();
            
            Debug.Log("Cработало через ОЬЖ");
        }
    }

    private void OnOpenVideo()
    {
      
        Time.timeScale = 0;
        _soundMuteHandler.OnVideoOpened();
        _buttonAd.interactable = false;
        _buttonShowAdv.SetActive(false);
    }

    private void OnRewarded()
    {
        Time.timeScale = 0;
     //   _soundMuteHandler.OnVideoOpened();
        _shop.ReceivingAward(_rewardViewingAds);
        Debug.Log("РЕВАРД");

        if (Input.GetKeyDown("space"))
        {
            _soundMuteHandler.OnVideoOpened();
            Debug.Log("Cработало в реварде");
        }
    }

    private void OnClose()
    {
        Time.timeScale = 1;
        _soundMuteHandler.OnVideoClosed();
    }
}
