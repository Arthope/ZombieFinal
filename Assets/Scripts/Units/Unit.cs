using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _damageValue;
    [SerializeField] private float _attackDistance;
    [SerializeField] private GameObject _healthBarPrefab;
    [SerializeField] private GameObject _flashPrefab;
    [SerializeField] private UnityEvent _onDied;
    private HealthBar _healthBar;

    public event UnityAction Died
    {
        add => _onDied.AddListener(value);
        remove => _onDied.RemoveListener(value);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _healthBar.SetHealth(_health, _maxHealth);
        if (_health <= 0)
        {
            Instantiate(_flashPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            _onDied.Invoke();
        }
    }

    public virtual void Start()
    {      
        _maxHealth = _health;
        GameObject healthBar = Instantiate(_healthBarPrefab);
        _healthBar = healthBar.GetComponent<HealthBar>();
         _healthBar.Setup(transform);
    }

    private void OnDestroy()
    {
        if (_healthBar)
        {
            Destroy(_healthBar.gameObject);
        }
    }

}
