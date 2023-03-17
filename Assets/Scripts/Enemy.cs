using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    WalkToUnit,
    Attack,
    Die
}

public class Enemy : MonoBehaviour
{
    private const string AttackParameter = "Attack";
    private const string RunParametr = "Run";
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private Unit TargetUnit;
    [SerializeField] private float _distanceToAttack;
    [SerializeField] private float _attackPeriod;
    [SerializeField] private GameObject _healthBarPrefab;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _flash;
    [SerializeField] private AudioSource _audioSource;
    private float _timer = 0f;
    public EnemyState CurrentEnemyState;
    public NavMeshAgent navMeshAgent;
    private HealthBar _healthBar;

    private void Start()
    {
        SetState(EnemyState.WalkToUnit);
        _maxHealth = _health;
        GameObject healthBar = Instantiate(_healthBarPrefab);
        _healthBar = healthBar.GetComponent<HealthBar>();
        _healthBar.Setup(transform);

    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _healthBar.SetHealth(_health, _maxHealth);
        if (_health <= 0)
        {
          Instantiate(_flash, transform.position, Quaternion.identity);

          Destroy(gameObject);
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
        
        FindClosestUnit();
        if (CurrentEnemyState == EnemyState.Idle)
        {
            FindClosestUnit();
        }
        else if (CurrentEnemyState == EnemyState.WalkToUnit)
        {     
            if (TargetUnit)
            {
                FindClosestUnit();
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
            //    navMeshAgent.SetDestination(TargetUnit.transform.position);
         //       float distance = Vector3.Distance(transform.position, TargetUnit.transform.position);
          //      if (distance > _distanceToAttack)
         //       {
         //           SetState(EnemyState.WalkToUnit);
        //        }
                _timer += Time.deltaTime;
                if (_timer > _attackPeriod)
                {
                    _timer = 0;
                    TargetUnit.TakeDamage(1);
                }
            }
            else
            {
                SetState(EnemyState.Idle);
            }
        }
    }

    public void SetState(EnemyState enemyState)
    {

        CurrentEnemyState = enemyState;
        if (CurrentEnemyState == EnemyState.Idle)
        {

        }
        else if (CurrentEnemyState == EnemyState.WalkToUnit)
        {
            if (TargetUnit)
            {
            FindClosestUnit();
            navMeshAgent.SetDestination(TargetUnit.transform.position);
                _audioSource.Play();
            }
            
        }
        else if (CurrentEnemyState == EnemyState.Attack)
        {
           
         
        }
        else if (CurrentEnemyState == EnemyState.Die)
        {

        }
    }

    public void FindClosestUnit()
    {
        Unit[] allUnits = FindObjectsOfType<Unit>();

        float minDistance = Mathf.Infinity;
        Unit closestUnit = null;

        for (int i = 0; i < allUnits.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, allUnits[i].transform.position);
            if(distance < minDistance)
            {
                minDistance = distance;
                closestUnit = allUnits[i];
            }
        }
        TargetUnit = closestUnit;
    }

    public void HideFlash()
    {
        _flash.SetActive(false);
    }
}
