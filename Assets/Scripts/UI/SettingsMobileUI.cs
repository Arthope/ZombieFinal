using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.WebUtility;
using Agava.YandexGames;

public class SettingsMobileUI : MonoBehaviour
{
    [SerializeField] private GameObject _menuRoll;
    [SerializeField] private GameObject _menuAutorization;

    void Start()
    {
        if (Agava.WebUtility.Device.IsMobile)
        {
            _menuRoll.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            _menuAutorization.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        }
    }
}
