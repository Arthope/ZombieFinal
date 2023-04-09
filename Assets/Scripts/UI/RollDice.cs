using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollDice : MonoBehaviour
{
    public int Number = 0;
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] sprites;

    public void RollNumber()
    {
        Number = Random.Range(1, sprites.Length);
        image.sprite = sprites[Number];
    }
}
