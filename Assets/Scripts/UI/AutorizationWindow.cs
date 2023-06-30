using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;

public class AutorizationWindow : MonoBehaviour
{
    [SerializeField] private GameObject _autorizationWindow;

    public void CloseWindowAutorization()
    {
        _autorizationWindow.gameObject.SetActive(false);
    }

    public void Autorization()
    {
        PlayerAccount.Authorize();
    }
}
