using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] CoinManager _coinManager;
    [SerializeField] GameObject _buttonChartersGirl;
    [SerializeField] GameObject _buttonChartersBoy;
    [SerializeField] GameObject _buttonChartersPolice;
    [SerializeField] GameObject _buttonChartersSolder;
    [SerializeField] GameObject _buttonChartersHero;
    private int priceGirl = 120;
    private int priceBoy = 240;
    private int pricePolice = 350;
    private int priceSolder= 500;
    private int priceHero = 700;

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

    public void BueGirl()
    {
        if (_coinManager.NumberOfCoins >= priceGirl)
        {
            _coinManager.NumberOfCoins -= priceGirl;
            PlayerPrefs.SetInt("numberGirl", 1);
            PlayerPrefs.SetInt("Characters", 1);
            Destroy(_buttonChartersGirl);
            PlayerPrefs.Save();
        }
    }

    public void BueBoy()
    {
        if (_coinManager.NumberOfCoins >= priceBoy)
        {
            _coinManager.NumberOfCoins -= priceBoy;
            PlayerPrefs.SetInt("numberBoy", 2);
            PlayerPrefs.SetInt("Characters", 2);
            Destroy(_buttonChartersBoy);
            PlayerPrefs.Save();

        }
    }

    public void BuePolice()
    {
        if (_coinManager.NumberOfCoins >= pricePolice)
        {
            _coinManager.NumberOfCoins -= pricePolice;
            PlayerPrefs.SetInt("numberPolice", 3);
            PlayerPrefs.SetInt("Characters", 3);
            Destroy(_buttonChartersPolice);
            PlayerPrefs.Save();
        }
    }

    public void BueSolder()
    {
        if (_coinManager.NumberOfCoins >= priceSolder)
        {
            _coinManager.NumberOfCoins -= priceSolder;
            PlayerPrefs.SetInt("numberSolder", 4);
            PlayerPrefs.SetInt("Characters", 4);
            Destroy(_buttonChartersSolder);
            PlayerPrefs.Save();
        }
    }

    public void BueHero()
    {
        if (_coinManager.NumberOfCoins >= priceHero)
        {
            PlayerPrefs.Save();
            _coinManager.NumberOfCoins -= priceHero;
            PlayerPrefs.SetInt("numberHero", 5);
            PlayerPrefs.SetInt("Characters", 5);
            Destroy(_buttonChartersHero);
        }
    }  
}
