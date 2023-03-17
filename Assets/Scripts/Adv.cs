using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Agava.YandexGames;
using Agava.YandexGames.Samples;

public class Adv : MonoBehaviour
{
    [SerializeField] CoinManager _coinManager;
    
    public void AddCoins()
    {
     //   PlayerPrefs.SetInt("_saveNumberOfCoin", _coinManager.NumberOfCoins += 50);
      
        PlayerPrefs.Save();
    }
}
