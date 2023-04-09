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
    [SerializeField] private List<Unit> Characters;
    [SerializeField] private List<Unit> UnitList = new List<Unit>();
    private int onDestroy = 0;
    private int _charaterId;

    public void CreatCharacter()
    {
        _charaterId = PlayerPrefs.GetInt("Characters");

        for (int i = 0; i < _dice.Number; i++)
        {
            Vector3 _position = new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-13f, -11f));
            Unit newUnit = Instantiate(Characters[_charaterId], _position, Quaternion.identity);
            newUnit.Died += CountDestroyed;
            UnitList.Add(newUnit);
        }
    }

    private void CountDestroyed()
    {
        onDestroy++;
        if (_dice.Number == onDestroy)
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
