using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private CoinDispley _coinDispley;
    [SerializeField] private GameObject _buttonChartersGirl;
    [SerializeField] private GameObject _buttonChartersBoy;
    [SerializeField] private GameObject _buttonChartersPolice;
    [SerializeField] private GameObject _buttonChartersSolder;
    [SerializeField] private GameObject _buttonChartersHero;
    private int _priceGirl = 120;
    private int _priceBoy = 240;
    private int _pricePolice = 350;
    private int _priceSolder= 500;
    private int _priceHero = 700;
    private int _currentCountCoinsPlayers = 0;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("_saveNumberOfCoin"))
        {
            _currentCountCoinsPlayers = PlayerPrefs.GetInt("_saveNumberOfCoin");
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("numberGirl"))
        {
            Destroy(_buttonChartersGirl);
        }
        if (PlayerPrefs.HasKey("numberBoy"))
        {
            Destroy(_buttonChartersBoy);
        }
        if (PlayerPrefs.HasKey("numberPolice"))
        {
            Destroy(_buttonChartersPolice);
        }
        if (PlayerPrefs.HasKey("numberSolder"))
        {
            Destroy(_buttonChartersSolder);
        }
        if (PlayerPrefs.HasKey("numberHero"))
        {
            Destroy(_buttonChartersHero);
        }
    }

    public void BueUnit(int PriceUnit, GameObject ButtonUnit, int numberCharters)
    {
        if (_currentCountCoinsPlayers >= PriceUnit)
        {
            PlayerPrefs.SetInt("_saveNumberOfCoin", _currentCountCoinsPlayers -= PriceUnit);
            PlayerPrefs.SetInt(name, numberCharters);
            PlayerPrefs.SetInt("Characters", numberCharters);
            Destroy(ButtonUnit);
            PlayerPrefs.Save();
            _coinDispley.UpdateCoins();
        }
    }

    public void BueGirl()
    {
        BueUnit(_priceGirl, _buttonChartersGirl, 1);
        PlayerPrefs.SetInt("numberGirl", 1);
    }

    public void BueBoy()
    {
        BueUnit(_priceBoy, _buttonChartersBoy, 2);
        PlayerPrefs.SetInt("numberBoy", 2);
    }

    public void BuePolice()
    {
        BueUnit(_pricePolice, _buttonChartersPolice, 3);
        PlayerPrefs.SetInt("numberPolice", 3);
    }

    public void BueSolder()
    {
        BueUnit(_priceSolder, _buttonChartersSolder, 4);
        PlayerPrefs.SetInt("numberSolder", 4);
    }

    public void BueHero()
    {
        BueUnit(_priceHero, _buttonChartersHero, 5);
        PlayerPrefs.SetInt("numberHero", 5);
    }  
}
