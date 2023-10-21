using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _navMeshAgent;


    void Start()
    {
        _navMeshAgent.SetDestination(transform.position);
    }

    
}
