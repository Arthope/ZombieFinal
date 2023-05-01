using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollDice : MonoBehaviour
{
    [SerializeField] private int _number;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _sprites;

    public int Number => _number;

    public void RollNumber()
    {
        _number = Random.Range(0, _sprites.Length);
        _image.sprite = _sprites[_number];
    }
}
