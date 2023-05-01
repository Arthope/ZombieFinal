using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using TMPro;

public class CoinDispley : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textCoins;
    [SerializeField] private GameObject _buttonShowAdv;
    [SerializeField] private int _numberOfCoins;
    private int _saveNumberOfCoin;

    public int NumberOfCoins => _numberOfCoins;

    private void Start()
    {
        if (PlayerPrefs.HasKey("_saveNumberOfCoin"))
        {
            _numberOfCoins = PlayerPrefs.GetInt("_saveNumberOfCoin");
        }
        UpdateCoins();
        transform.parent = null;
    }

    public void UpdateCoins()
    {
        _textCoins.text = _numberOfCoins.ToString();
    }
}
