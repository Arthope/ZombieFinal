using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Education : MonoBehaviour
{
    [SerializeField] GameObject _gameObjectOn;
    [SerializeField] GameObject _gameObjectOff;

    public void EducationOn()
    {
        _gameObjectOn.SetActive(true);
    }

    public void EducationOff()
    {
        _gameObjectOff.SetActive(false);
    }
}
