using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _textLevel;
    [SerializeField] private GameObject _menuWin;
    [SerializeField] private GameObject _menulose;
    [SerializeField] private RollDice _dice;
    [SerializeField] private List<Unit> _characters;
    [SerializeField] private List<Unit> _unitList = new List<Unit>();
    private int _onDestroy = 0;
    private int _numberCharters;
    private float _coordinateMinimalX = -3f;
    private float _coordinateMaximalX = 3f;
    private float _coordinateY = 0f;
    private float _coordinateMinimalZ = -9f;
    private float _coordinateMaximalZ = -11f;
    private int _charaterId;

    public void CreatCharacter()
    {
        _charaterId = PlayerPrefs.GetInt("Characters");
        _numberCharters = _dice.Number + 1;

        for (int i = 0; i < _numberCharters; i++)
        {
            Vector3 _position = new Vector3(Random.Range(_coordinateMinimalX, _coordinateMaximalX), _coordinateY, Random.Range(_coordinateMinimalZ, _coordinateMaximalZ));
            Unit newUnit = Instantiate(_characters[_charaterId], _position, Quaternion.identity);
            newUnit.Died += CountDestroyed;
            _unitList.Add(newUnit);
        }
    }

    private void CountDestroyed()
    {
        _onDestroy++;
        if (_numberCharters == _onDestroy)
        {
            EnemyWin();
        }
    }

    private void OnDisable()
    {
        foreach (var Unit in _unitList)
            Unit.Died -= CountDestroyed;
    }

    private void EnemyWin()
    {
        Time.timeScale = 0f;
        _menulose.SetActive(true);
        _textLevel.SetActive(false);
    }
}
