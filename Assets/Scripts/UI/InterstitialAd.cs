using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using Agava.YandexGames.Samples;

public class InterstitialAd : MonoBehaviour
{
    [SerializeField] private SoundMuteHandler _soundMuteHandler;

    private void Start()
    {
        ShowAdv();
    }

    public void ShowAdv()
    {
        Agava.YandexGames.InterstitialAd.Show(Open, Close);
    }

    private void Close(bool close)
    {
        if (close)
        {
            _soundMuteHandler.OnVideoClosed();
        }
    }

    private void Open()
    {
        _soundMuteHandler.OnVideoOpened();

    }
}
