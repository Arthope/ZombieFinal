using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Agava.YandexGames;
using Lean.Localization;

public class InternacionaleText : MonoBehaviour
{
    [SerializeField] string _en;
    [SerializeField] string _ru;

    private IEnumerator Start()
    {
        while (!YandexGamesSdk.IsInitialized)
        {
            yield return new WaitForSeconds(0.05f);
            if (YandexGamesSdk.IsInitialized)
            {
                if (Language.Instance.CurrentLanguage == "en")
                {
                    GetComponent<TextMeshProUGUI>().text = _en;
                }
                else if (Language.Instance.CurrentLanguage == "ru")
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