using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{

    [SerializeField] private GameObject _textLevel;
    [SerializeField] private GameObject _menuWin;
    [SerializeField] private GameObject _menulose;
    [SerializeField] public RollDice _dice;
    private int onDestroy = 0;
    [SerializeField] private List<Unit> Characters;
    [SerializeField] private List<Unit> UnitList = new List<Unit>();
    private int numberCharters;
    private float coordinateMinimalX = -3f;
    private float coordinateMaximalX = 3f;
    private float coordinateY = 0f;
    private float coordinateMinimalZ = -9f;
    private float coordinateMaximalZ = -11f;

    private int _charaterId;
    public void CreatCharacter()
    {
        _charaterId = PlayerPrefs.GetInt("Characters");
        numberCharters = _dice.Number + 1;

        for (int i = 0; i < numberCharters; i++)
        {
            Vector3 _position = new Vector3(Random.Range(coordinateMinimalX, coordinateMaximalX), coordinateY, Random.Range(coordinateMinimalZ, coordinateMaximalZ));
            Unit newUnit = Instantiate(Characters[_charaterId], _position, Quaternion.identity);
            newUnit.Died += CountDestroyed;
            UnitList.Add(newUnit);
        }
    }

    private void CountDestroyed()
    {
        onDestroy++;
        if (numberCharters == onDestroy)
        {
            EnemyWin();
        }
    }

    private void OnDisable()
    {
        foreach (var Unit in UnitList)
            Unit.Died -= CountDestroyed;
    }

    private void EnemyWin()
    {
        Time.timeScale = 0f;
        _menulose.SetActive(true);
        _textLevel.SetActive(false);
    }
}
