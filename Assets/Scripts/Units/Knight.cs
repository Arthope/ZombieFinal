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
    [SerializeField] private Enemy TargetEnemy;
    [SerializeField] private float _distanceToAttack;
    [SerializeField] private float _attackPeriod;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<Enemy> EnemyList = new List<Enemy>();
    [SerializeField] private UnitState CurrentUnitState;
    [SerializeField] private NavMeshAgent navMeshAgent;
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

        if (CurrentUnitState == UnitState.WalkToEnemy)
        {
            if (TargetEnemy)
            {
                TargetEnemy = GetClosest(transform.position);
                navMeshAgent.SetDestination(TargetEnemy.transform.position);
                _animator.SetBool("Run", true);
                float distance = Vector3.Distance(transform.position, TargetEnemy.transform.position);
                if (distance <= _distanceToAttack)
                {
                    SetState(UnitState.Attack);
                }
            }
        }
        else if (CurrentUnitState == UnitState.Attack)
        {
            _animator.SetTrigger("Attack");
            if (TargetEnemy)
            {
                _timer += Time.deltaTime;
                if (_timer > _attackPeriod)
                {
                    _timer = 0;
                    TargetEnemy.TakeDamage(1);
                    _audioSource.pitch = Random.Range(0.8f, 1.2f);
                    _audioSource.Play();
                    if (TargetEnemy = null)
                    {
                        TargetEnemy = GetClosest(transform.position);
                    }
                }
            }
        }
    }

    public void SetState(UnitState unitState)
    {
        CurrentUnitState = unitState;
        TargetEnemy = GetClosest(transform.position);

        if (CurrentUnitState == UnitState.WalkToEnemy)
        {
            if (TargetEnemy)
            {
                TargetEnemy = GetClosest(transform.position);
                navMeshAgent.SetDestination(TargetEnemy.transform.position);

            }
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
}
