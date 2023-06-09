using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    Walk,
    Attack,
    Die
}

public abstract class CharacterMovement : MonoBehaviour
{
    private const string StateRun = "Run";
    private const string StateAttack = "Attack";
    public abstract Vector3 Target { get; }
    [SerializeField] private bool _itsShooter;
    [SerializeField] private State _currentState;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _distanceToAttack;
    [SerializeField] private float _attackPeriod;
    [SerializeField] private GameObject _flash;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private UnityEngine.AI.NavMeshAgent _navMeshAgent;
    private int _damage = 1;
    private float _minimalAudioPitch = 0.8f;
    private float _maximalAudioPitch = 1.2f;
    private float _timer = 0f;

    public void Start()
    {
        SetState(State.Walk);
    }

    public void Update()
    {
        if (Target != null)
        {
            GetClosest();
        }
       
        if (_currentState == State.Walk)
        {
            if (Target != Vector3.zero)
            {
                _navMeshAgent.SetDestination(Target);
                _animator.SetBool(StateRun, true);
                float distance = Vector3.Distance(transform.position, Target);
                if (distance <= _distanceToAttack)
                {
                    SetState(State.Attack);
                }

                if (Target == null)
                {
                    _currentState = State.Idle;

                }
            }
        }
        else if (_currentState == State.Attack)
        {
        
            if (_itsShooter = true)
            {
                transform.LookAt(Target);
            }

            _animator.SetTrigger(StateAttack);
            if (Target != Vector3.zero)
            {
                _timer += Time.deltaTime;

                if (_timer > _attackPeriod)
                {
                    _timer = 0;
                    DoDamage(_damage);
                    _audioSource.pitch = Random.Range(_minimalAudioPitch, _maximalAudioPitch);
                    _audioSource.Play();
                    if (_itsShooter = true)
                    {
                        _flash.SetActive(true);
                        Invoke("HideFlash", 0.08f);
                    }
                }
            }
        }
        else if (_currentState == State.Idle)
        {
            _animator.SetBool(StateRun, true);
        }
    }

    public void SetState(State state)
    {
        _currentState = state;

        if (_currentState == State.Walk)
        {
            if (Target != Vector3.zero)
            {
                GetClosest();
                _navMeshAgent.SetDestination(Target);
            }
        }
    }

    public void HideFlash()
    {
        _flash.SetActive(false);
    }

    public abstract void GetClosest();
    public abstract void DoDamage(int damage);
}
