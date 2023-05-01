using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ShootState
{
    Idle,
    WalkToEnemy,
    Attack,
    Die
}

public class Shooter : Unit
{
    [SerializeField] private Enemy _targetEnemy;
    [SerializeField] private float _distanceToAttack;
    [SerializeField] private float _attackPeriod;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _flash;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private List<Enemy> _enemyList = new List<Enemy>();
    [SerializeField] private ShootState _currentUnitState;
    private float _timer = 0f;

    public override void Start()
    {
        base.Start();
        SetState(ShootState.WalkToEnemy);
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

    private void Update()
    {
        _targetEnemy = GetClosest(transform.position);

        if (_currentUnitState == ShootState.WalkToEnemy)
        {
            if (_targetEnemy)
            {
                _targetEnemy = GetClosest(transform.position);
                float distance = Vector3.Distance(transform.position, _targetEnemy.transform.position);
                if (distance < _distanceToAttack)
                {
                    SetState(ShootState.Attack);
                }
            }
        }
        else if (_currentUnitState == ShootState.Attack)
        {
            if (_targetEnemy)
            {
                float distance = Vector3.Distance(transform.position, _targetEnemy.transform.position);
                if (distance > _distanceToAttack)
                {
                    SetState(ShootState.WalkToEnemy);
                }
                _timer += Time.deltaTime;
                if (_timer > _attackPeriod)
                {
                    _timer = 0;
                    _targetEnemy.TakeDamage(1);
                    _audioSource.pitch = Random.Range(0.8f, 1.2f);
                    _audioSource.Play();
                    _flash.SetActive(true);
                    Invoke("HideFlash", 0.08f);
                }
            }
            else
            {
                _targetEnemy = GetClosest(transform.position);
                SetState(ShootState.Idle);
            }
        }
        if (_targetEnemy)
        {
            transform.LookAt(_targetEnemy.transform.position);
        }
    }

    public void SetState(ShootState unitState)
    {
        _currentUnitState = unitState;
        _targetEnemy = GetClosest(transform.position);

        if (_currentUnitState == ShootState.WalkToEnemy)
        {
            _targetEnemy = GetClosest(transform.position);
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

    public void HideFlash()
    {
        _flash.SetActive(false);
    }
}
