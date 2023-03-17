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
    private float _timer = 0f;
    public ShootState CurrentUnitState;
    public NavMeshAgent navMeshAgent;

    public override void Start()
    {
        base.Start();
        SetState(ShootState.WalkToEnemy);
    }

    void Update()
    {
        FindClosestEnemy();
        if (CurrentUnitState == ShootState.Idle)
        {
            FindClosestEnemy();
        }
       else if (CurrentUnitState == ShootState.WalkToEnemy)
       {
           if (TargetEnemy)
           {
               FindClosestEnemy();
                navMeshAgent.SetDestination(TargetEnemy.transform.position);
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
                navMeshAgent.SetDestination(TargetEnemy.transform.position);
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
                FindClosestEnemy();
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
        if (CurrentUnitState == ShootState.Idle)
        {

        }
        else if (CurrentUnitState == ShootState.WalkToEnemy)
        {
            FindClosestEnemy();
       //     navMeshAgent.SetDestination(TargetEnemy.transform.position);

        }
        else if (CurrentUnitState == ShootState.Attack)
        {


        }
        else if (CurrentUnitState == ShootState.Die)
        {

        }
    }

    public void FindClosestEnemy()
    {
        Enemy[] allEnemys = FindObjectsOfType<Enemy>();

        float minDistance = Mathf.Infinity;
        Enemy closestEnemy = null;

        for (int i = 0; i < allEnemys.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, allEnemys[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = allEnemys[i];
            }
        }
        TargetEnemy = closestEnemy;
    }

    public void HideFlash()
    {
        _flash.SetActive(false);
    }
}
