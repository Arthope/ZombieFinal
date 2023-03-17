using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollDice : MonoBehaviour
{
    public Image image;

    public Sprite[] sprites;

    public int number;

    public void RollNumber()
    {
        number = Random.Range(0, sprites.Length);
        image.sprite = sprites[number];
    }
}
