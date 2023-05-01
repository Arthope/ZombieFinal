using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum UnitState
{
    Idle,
    WalkToEnemy,
    Attack,
    Die
}

public class Knight : Unit
{
    [SerializeField] private Enemy _targetEnemy;
    [SerializeField] private float _distanceToAttack;
    [SerializeField] private float _attackPeriod;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<Enemy> _enemyList = new List<Enemy>();
    [SerializeField] private UnitState _currentUnitState;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    private float _timer = 0f;

    public override void Start()
    {
        base.Start();
        SetState(UnitState.WalkToEnemy);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            _enemyList.Add(enemyComponent);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            _enemyList.Remove(enemyComponent);
        }
    }


    void Update()
    {
        _targetEnemy = GetClosest(transform.position);

        if (_currentUnitState == UnitState.WalkToEnemy)
        {
            if (_targetEnemy)
            {
                _targetEnemy = GetClosest(transform.position);
                _navMeshAgent.SetDestination(_targetEnemy.transform.position);
                _animator.SetBool("Run", true);
                float distance = Vector3.Distance(transform.position, _targetEnemy.transform.position);
                if (distance <= _distanceToAttack)
                {
                    SetState(UnitState.Attack);
                }
            }
        }
        else if (_currentUnitState == UnitState.Attack)
        {
            _animator.SetTrigger("Attack");
            if (_targetEnemy)
            {
                _timer += Time.deltaTime;
                if (_timer > _attackPeriod)
                {
                    _timer = 0;
                    _targetEnemy.TakeDamage(1);
                    _audioSource.pitch = Random.Range(0.8f, 1.2f);
                    _audioSource.Play();
                    if (_targetEnemy = null)
                    {
                        _targetEnemy = GetClosest(transform.position);
                    }
                }
            }
        }
    }

    public void SetState(UnitState unitState)
    {
        _currentUnitState = unitState;
        _targetEnemy = GetClosest(transform.position);

        if (_currentUnitState == UnitState.WalkToEnemy)
        {
            if (_targetEnemy)
            {
                _targetEnemy = GetClosest(transform.position);
                _navMeshAgent.SetDestination(_targetEnemy.transform.position);

            }
        }
    }

    public Enemy GetClosest(Vector3 point)
    {
        float minDistance = Mathf.Infinity;
        Enemy closestEnemy = null;

        foreach (Enemy go in _enemyList)
        {
            if (go != null)
            {
                float distance = Vector3.Distance(point, go.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = go;
                }
            }
        }
        return closestEnemy;
    }
}
