using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRoll1 : MonoBehaviour
{
    [SerializeField] private GameObject _levelText;

    public void OnText()
    {
        _levelText.SetActive(true);
    }
}
