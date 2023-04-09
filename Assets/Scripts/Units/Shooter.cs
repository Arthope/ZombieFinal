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
    [SerializeField] private Enemy TargetEnemy;
    [SerializeField] private float _distanceToAttack;
    [SerializeField] private float _attackPeriod;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] GameObject _flash;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private GameObject _center;
    [SerializeField] List<Enemy> EnemyList = new List<Enemy>();
    public Enemy[] allEnemys;
    private float _timer = 0f;
    public ShootState CurrentUnitState;

    public override void Start()
    {
        base.Start();
        SetState(ShootState.WalkToEnemy);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            EnemyList.Add(enemyComponent);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            EnemyList.Remove(enemyComponent);
        }
    }

    void Update()
    {
        TargetEnemy = GetClosest(transform.position);

        if (CurrentUnitState == ShootState.WalkToEnemy)
        {
            if (TargetEnemy)
            {
                TargetEnemy = GetClosest(transform.position);
                float distance = Vector3.Distance(transform.position, TargetEnemy.transform.position);
                if (distance < _distanceToAttack)
                {
                    SetState(ShootState.Attack);
                }
            }
        }
        else if (CurrentUnitState == ShootState.Attack)
        {
            if (TargetEnemy)
            {
                float distance = Vector3.Distance(transform.position, TargetEnemy.transform.position);
                if (distance > _distanceToAttack)
                {
                    SetState(ShootState.WalkToEnemy);
                }
                _timer += Time.deltaTime;
                if (_timer > _attackPeriod)
                {
                    _timer = 0;
                    TargetEnemy.TakeDamage(1);
                    _audioSource.pitch = Random.Range(0.8f, 1.2f);
                    _audioSource.Play();
                    _flash.SetActive(true);
                    Invoke("HideFlash", 0.08f);
                }
            }
            else
            {
                TargetEnemy = GetClosest(transform.position);
                SetState(ShootState.Idle);
            }
        }
        if (TargetEnemy)
        {
            transform.LookAt(TargetEnemy.transform.position);
        }
    }

    public void SetState(ShootState unitState)
    {
        CurrentUnitState = unitState;
        TargetEnemy = GetClosest(transform.position);

        if (CurrentUnitState == ShootState.WalkToEnemy)
        {
            TargetEnemy = GetClosest(transform.position);
        }

    }

    public Enemy GetClosest(Vector3 point)
    {
        float minDistance = Mathf.Infinity;
        Enemy closestEnemy = null;

        foreach (Enemy go in EnemyList)
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
