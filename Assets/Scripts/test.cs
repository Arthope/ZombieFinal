using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private GameObject _textLevel;
    [SerializeField] private GameObject _menuWin;
    [SerializeField] private GameObject _menulose;
    [SerializeField] Progress _progress;
   
    public void Update()
    {
        Unit[] allUnits = FindObjectsOfType<Unit>();
        Enemy[] allEnemys = FindObjectsOfType<Enemy>();

        if (allEnemys.Length == 0)
        {
            Time.timeScale = 0f;
            _textLevel.SetActive(false);
            _menuWin.SetActive(true);
        }

        if (allUnits.Length == 0)
        {
            Time.timeScale = 0f;
            _menulose.SetActive(true);
            _textLevel.SetActive(false);

        }
    }
}
