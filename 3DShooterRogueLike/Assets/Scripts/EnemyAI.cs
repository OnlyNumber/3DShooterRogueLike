using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    

    void Start()
    {
        _navMeshAgent.SetDestination(transform.position);
    }

    private void Update()
    {
        _canvas.transform.LookAt(new Vector3(_player.position.x, _canvas.transform.position.y, _player.position.z));

        //_canvas.transform.Rotate(new Vector3(180,0,0));
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
            _animator.SetBool("Attack",true);
        }
        

    
    
    }

    private void Attack()
    {
        Collider[] hits = Physics.OverlapSphere(_attackPosition.position, 1, _attackLayer);

        foreach (var hit in hits)
        {
            hit.gameObject.GetComponent<HealthHandler>().TakeDamage(_damage);
        }

    }



}
