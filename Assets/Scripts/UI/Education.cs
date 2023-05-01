using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Education : MonoBehaviour
{
    [SerializeField] private GameObject _secondEducationOn;
    [SerializeField] private GameObject _firstEducatuonOff;

    public void EducationOn()
    {
        _secondEducationOn.SetActive(true);
    }

    public void EducationOff()
    {
        _firstEducatuonOff.SetActive(false);
    }
}
