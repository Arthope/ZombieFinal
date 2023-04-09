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
    [SerializeField] private Unit TargetUnit;
    [SerializeField] private float _distanceToAttack;
    [SerializeField] private float _attackPeriod;
    [SerializeField] private GameObject _healthBarPrefab;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _flash;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private EnemyState CurrentEnemyState;
    [SerializeField] private UnityEvent _onDied;
    [SerializeField] private List<Unit> Units;
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
            Units.Add(unitComponent);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<Unit>(out Unit unitComponent))
        {
            Units.Remove(unitComponent);
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
          _onDied.Invoke();
        }
    }

    private void OnDestroy()
    {
        if (_healthBar)
        {
            Destroy(_healthBar.gameObject);
        }
    }

    void Update()
    {
        TargetUnit = GetClosest(transform.position);

        if (CurrentEnemyState == EnemyState.WalkToUnit)
        {
            if (TargetUnit)
            {
                _audioSource.Play();
                navMeshAgent.SetDestination(TargetUnit.transform.position);
                float distance = Vector3.Distance(transform.position, TargetUnit.transform.position);
                _animator.SetBool("Run", true);
                if (distance < _distanceToAttack)
                {
                    SetState(EnemyState.Attack);
                }
            }
        }
        else if (CurrentEnemyState == EnemyState.Attack)
        {
            _animator.SetTrigger("Attack");
            if (TargetUnit)
            {
                _timer += Time.deltaTime;
                if (_timer > _attackPeriod)
                {
                    if (TargetUnit._health == 0)
                    {
                        TargetUnit = null;
                    }
                    else
                    {
                        _timer = 0;
                        TargetUnit.TakeDamage(1);
                    }
                }
            }
        }
    }

    public void SetState(EnemyState enemyState)
    {
        CurrentEnemyState = enemyState;

        if (CurrentEnemyState == EnemyState.WalkToUnit)
        {
            TargetUnit = GetClosest(transform.position);
            if (TargetUnit)
            {
                TargetUnit = GetClosest(transform.position);
                navMeshAgent.SetDestination(TargetUnit.transform.position);
                _audioSource.Play();
            }
        }
    }

    public Unit GetClosest(Vector3 point)
    {
        float minDistance = Mathf.Infinity;
        Unit closestUnit = null;

        foreach (Unit go in Units)
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
