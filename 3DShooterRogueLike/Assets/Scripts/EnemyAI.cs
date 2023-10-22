using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _navMeshAgent;

    [SerializeField]
    private Transform _player;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private Canvas _canvas;

    [SerializeField]
    private float _normalSpeed;

    [SerializeField]
    private float _speedWhileAttack;

    [SerializeField]
    private Transform _attackPosition;

    [SerializeField]
    private LayerMask _attackLayer;

    [SerializeField]
    private float _damage;

    [field: SerializeField]
    public HealthHandler HealthHandler { get; private set; }

    private Action _currentState;

    [SerializeField]
    private float _searchRadius;

    private GameManager _gameManager;

    [SerializeField]
    private List<Item> _item;

    void Start()
    {
        _navMeshAgent.SetDestination(transform.position);

        HealthHandler.OnDeath += Death;

        HealthHandler.OnDeath += SpawnItem;

        StartCoroutine(StartAfterStart());

        _currentState += SearchPlayerState;


    }

    public void Initialize(GameManager gameManager)
    {
        _gameManager = gameManager;
    }


    private void SearchPlayerState()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _searchRadius, _attackLayer);

        if (hits.Length > 0)
        {
            foreach (var item in hits)
            {
                _player = item.gameObject.transform;
            }

            _currentState -= SearchPlayerState;

            _currentState += CombatState;
            
            HealthHandler.OnHealthChange -= GetPlayer;
        }



    }

    private void CombatState()
    {
        _canvas.transform.LookAt(new Vector3(_player.position.x, _canvas.transform.position.y, _player.position.z));

        _animator.SetFloat("Speed", _navMeshAgent.speed);

        if (Vector3.Distance(transform.position, _player.position) > 1.5f)
        {
            _navMeshAgent.speed = _normalSpeed;

            _navMeshAgent.SetDestination(_player.position);

            _animator.SetBool("Attack", false);
        }
        else
        {
            _navMeshAgent.speed = 0;
            _navMeshAgent.SetDestination(_player.position);
            _animator.SetBool("Attack", true);
        }
    }

    private void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            _navMeshAgent.SetDestination(transform.position);

            return;
        }


        _currentState?.Invoke();
    }

    private void Attack()
    {
        Collider[] hits = Physics.OverlapSphere(_attackPosition.position, 1, _attackLayer);

        foreach (var hit in hits)
        {
            hit.gameObject.GetComponent<HealthHandler>().TakeDamage(_damage);
        }

    }

    public void Death()
    {
        _animator.SetTrigger("Death");
        StartCoroutine(SelfDestroy());

    }

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(2f);

        _gameManager.Recicle(this);

        _gameManager.CheckWinCondition();

    }

    private IEnumerator StartAfterStart()
    {

        yield return new WaitForSeconds(0.5f);

        HealthHandler.OnHealthChange += GetPlayer;

    }

    private void GetPlayer()
    {
        Debug.Log("GetPlayer");

        _player = _gameManager.GetPlayer().transform;

        _currentState -= SearchPlayerState;

        _currentState += CombatState;

        HealthHandler.OnHealthChange -= GetPlayer;
    }

    private void SpawnItem()
    {
        Instantiate(_item[UnityEngine.Random.Range(0, _item.Count)], transform.position, Quaternion.identity);
    }

}
