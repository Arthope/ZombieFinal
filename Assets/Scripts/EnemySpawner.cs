using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _textLevel;
    [SerializeField] private GameObject _menuWin;
    [SerializeField] private GameObject _menulose;
    [SerializeField] private int _numberEnemy;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private List<Enemy> EnemyList = new List<Enemy>();
    private int onDestroy = 0;

    public void CreatEnemy()
    {
        for (int i = 0; i < _numberEnemy; i++)
        {
            Vector3 _position = new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(4f, -4f));
            Enemy newEnemy = Instantiate(_enemyPrefab, _position, Quaternion.identity);
            newEnemy.DiedEnemy += CountDestroyed;
            EnemyList.Add(newEnemy);
        }
    }

    private void CountDestroyed()
    {
        onDestroy ++;
        if (_numberEnemy == onDestroy)
        {
            UnitsWin();
        }
    }

    private void OnDisable()
    {
        foreach (var enemy in EnemyList)
            enemy.DiedEnemy -= CountDestroyed;
        Debug.Log(onDestroy + " юнитспавнер");
    }

    private void UnitsWin()
    {
        Time.timeScale = 0f;
        _textLevel.SetActive(false);
        _menuWin.SetActive(true);
    }
}
