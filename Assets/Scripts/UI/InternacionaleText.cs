using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Agava.YandexGames;
using Lean.Localization;

public class InternacionaleText : MonoBehaviour
{
    [SerializeField] private string _en;
    [SerializeField] private string _ru;
    string CurrentLanguage;

    private IEnumerator Start()
    {
        while (!YandexGamesSdk.IsInitialized)
        {
            yield return new WaitForSeconds(0.05f);

            if (YandexGamesSdk.IsInitialized)
            {
                CurrentLanguage = YandexGamesSdk.Environment.i18n.lang;
                if (CurrentLanguage == "en")
                {
                    GetComponent<TextMeshProUGUI>().text = _en;
                }
                else if (CurrentLanguage == "ru")
                {
                    GetComponent<TextMeshProUGUI>().text = _ru;
                }
                else
                {
                    GetComponent<TextMeshProUGUI>().text = _ru;
                }
            }
        }
    }
}