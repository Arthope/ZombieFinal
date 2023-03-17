using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Agava.YandexGames;
using Agava.YandexGames.Samples;

public class Init : MonoBehaviour
{
    public int next;

   private void Awake()
   {
       YandexGamesSdk.CallbackLogging = true;
   }

    private IEnumerator Start()
    {
      //  if (!YandexGamesSdk.IsInitialized)
       
            yield return Agava.YandexGames.YandexGamesSdk.Initialize(OnInitialized);
    }

    private void OnInitialized()
    {
            SceneManager.LoadScene(1);

    }
}
