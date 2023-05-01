using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public enum EnemyState
{
    Idle,
    WalkToUnit,
    Attack,
    Die
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private Unit _targetUnit;
    [SerializeField] private float _distanceToAttack;
    [SerializeField] private float _attackPeriod;
    [SerializeField] private GameObject _healthBarPrefab;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _flash;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private EnemyState _currentEnemyState;
    [SerializeField] private UnityEvent _onDied;
    [SerializeField] private List<Unit> _units;
    private float _timer = 0f;
    private HealthBar _healthBar;

    public event UnityAction DiedEnemy
    {
        add => _onDied.AddListener(value);
        remove => _onDied.RemoveListener(value);
    }

    private void Start()
    {
        SetState(EnemyState.WalkToUnit);
        _maxHealth = _health;
        GameObject healthBar = Instantiate(_healthBarPrefab);
        _healthBar = healthBar.GetComponent<HealthBar>();
        _healthBar.Setup(transform);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<Unit>(out Unit unitComponent))
        {
            _units.Add(unitComponent);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<Unit>(out Unit unitComponent))
        {
            _units.Remove(unitComponent);
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _healthBar.SetHealth(_health, _maxHealth);
        if (_health <= 0)
        {
          Instantiate(_flash, transform.position, Quaternion.identity);
          Destroy(gameObject);
     //     _onDied.Invoke();
        }
    }

    private void OnDestroy()
    {
        if (_healthBar)
        {
            Destroy(_healthBar.gameObject);
            _onDied.Invoke();
        }
    }

    void Update()
    {
        _targetUnit = GetClosest(transform.position);

        if (_currentEnemyState == EnemyState.WalkToUnit)
        {
            if (_targetUnit)
            {
                _audioSource.Play();
                _navMeshAgent.SetDestination(_targetUnit.transform.position);
                float distance = Vector3.Distance(transform.position, _targetUnit.transform.position);
                _animator.SetBool("Run", true);
                if (distance < _distanceToAttack)
                {
                    SetState(EnemyState.Attack);
                }
            }
        }
        else if (_currentEnemyState == EnemyState.Attack)
        {
            _animator.SetTrigger("Attack");
            if (_targetUnit)
            {
                _timer += Time.deltaTime;
                if (_timer > _attackPeriod)
                {
                    {
                        _timer = 0;
                        _targetUnit.TakeDamage(1);
                    }
                }
            }
        }
    }

    public void SetState(EnemyState enemyState)
    {
        _currentEnemyState = enemyState;

        if (_currentEnemyState == EnemyState.WalkToUnit)
        {
            _targetUnit = GetClosest(transform.position);
            if (_targetUnit)
            {
                _targetUnit = GetClosest(transform.position);
                _navMeshAgent.SetDestination(_targetUnit.transform.position);
                _audioSource.Play();
            }
        }
    }

    public Unit GetClosest(Vector3 point)
    {
        float minDistance = Mathf.Infinity;
        Unit closestUnit = null;

        foreach (Unit go in _units)
        {
            if (go != null)
            {
                float distance = Vector3.Distance(point, go.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestUnit = go;
                }
            }
        }
        return closestUnit;
    }
}
