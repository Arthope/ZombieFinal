using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] public RollDice _dice;
    public List<GameObject> Characters;
    
    private int _charaterId;

    public void CreatCharacter()
    {
        _charaterId = PlayerPrefs.GetInt("Characters");

        for (int i = 0; i < _dice.number + 1; i++)
        {
            Debug.Log(_dice.number);
            Vector3 _position = new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-13f, -11f));
            Instantiate(Characters[_charaterId], _position, Quaternion.identity);
        }
    }
}
